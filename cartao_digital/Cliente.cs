public class Cliente
{
    /*
        O que e um cliente?
        Que acoes um cliente pode fazer?
        Quais informacoes um cliente tem?
        Quais comportamentos um cliente tem?
        Quais atributos um cliente tem?
        Quais metodos um cliente tem?
        Quais propriedades um cliente tem?
        Quais informacoes ele pode acessar?
        Quais informacoes ele pode modificar?
        Quais informacoes ele pode criar?
        Quais informacoes ele pode deletar?

        Com relacao a criacao de um cliente, o que significa sucesso?
    */

    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Saldo { get; private set; } // Encapsulamento do saldo


    // Metodo construtor
    public Cliente(int id, string nome)
    {
        if (string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("Nome nao pode ser nulo ou vazio");
        }

        Id = id;
        Nome = nome;
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentOutOfRangeException((nameof(valor)), "Valor de deposito deve ser maior que zero");
        }

        Saldo += valor;
    }

    public virtual void Sacar(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentOutOfRangeException((nameof(valor)), "Valor de saque deve ser maior que zero");
        }

        var tarifa = CalcularTarifaSaque(valor);
        var valorTotal = valor + tarifa;

        if (valorTotal > Saldo)
        {
            throw new InvalidOperationException("Saldo insuficiente para saque");
        }

        Saldo -= valorTotal;
    }

    public void Transferir(Cliente clienteDestino, decimal valor)
    {
        if (clienteDestino is null)
            throw new ArgumentNullException(nameof(clienteDestino), "Cliente destino nao pode ser nulo");

        this.Sacar(valor); // Saca do cliente origem
        clienteDestino.Depositar(valor);
    }

    // Colocamos o virtual para permitir que classes derivadas (filhas) possam sobrescrever esse metodo (override)
    // Metodo que sofra o POLIMORFISMO
    public virtual decimal CalcularTarifaSaque(decimal valor) => 0m;

    public override string ToString() => $"Cliente: {Nome}, Saldo: {Saldo:C}";
}

// Cliente PF
public class ClientePF : Cliente
{

    public string CPF { get; set; }

    public ClientePF(int id, string nome, string cpf)
        : base(id, nome) // base vem da heranca, chamando o construtor da classe base (Cliente)
    {
        if (string.IsNullOrEmpty(cpf))
        {
            throw new ArgumentException("CPF nao pode ser nulo ou vazio");
        }

        CPF = cpf;
    }

    public override decimal CalcularTarifaSaque(decimal valor) => 1.00m; // Tarifa fixa de R$ 1,00 para Cliente PF

    public override string ToString() => $"Cliente PF: {Nome}, CPF: {CPF}, Saldo: {Saldo:C}";
}

// Cliente PJ
public class ClientePJ : Cliente
{
    public string CNPJ { get; set; }

    public ClientePJ(int id, string nome, string cnpj)
        : base(id, nome) // base vem da heranca, chamando o construtor da classe base (Cliente)
    {
        if (string.IsNullOrEmpty(cnpj))
        {
            throw new ArgumentException("CNPJ nao pode ser nulo ou vazio");
        }

        CNPJ = cnpj;
    }

    public override decimal CalcularTarifaSaque(decimal valor)
    {
        var tarifa = valor * 0.02m; // 2% do valor sacado
        return tarifa < 2.00m ? 2.00m : tarifa; // Tarifa minima de R$ 2,00 
    }

    public override string ToString() => $"Cliente PJ: {Nome}, CNPJ: {CNPJ}, Saldo: {Saldo:C}";
}

public class ClientePersonalite : Cliente
{
    private DateOnly _dataUltimoSaque;
    private decimal _totalSaqueHoje;
    public decimal LimiteSaqueDiario { get; set; }
    public ClientePersonalite(int id, string nome, decimal limiteSaqueDiario) : base(id, nome)
    {
        if (limiteSaqueDiario <= 0)
            throw new ArgumentOutOfRangeException(nameof(limiteSaqueDiario), "Limite de saque diario deve ser maior que zero");

        LimiteSaqueDiario = limiteSaqueDiario;
        _dataUltimoSaque = DateOnly.MinValue;
        _totalSaqueHoje = 0m;
    }

    public override decimal CalcularTarifaSaque(decimal valor) => 0m; // Isento de tarifa

    public override void Sacar(decimal valor)
    {
        var hoje = DateOnly.FromDateTime(DateTime.UtcNow);
        if (hoje != _dataUltimoSaque)
        {
            _dataUltimoSaque = hoje;
            _totalSaqueHoje = 0m; // Reseta o total de saque diario
        }

        if (_totalSaqueHoje + valor > LimiteSaqueDiario)
        {
            throw new InvalidOperationException("Limite de saque diario excedido");
        }

        base.Sacar(valor); // Chama o metodo Sacar da classe base (Cliente)
        _totalSaqueHoje += valor;
    }

    public override string ToString() => $"Cliente Personalite: {Nome}, Limite Saque Diario: {LimiteSaqueDiario:C}, Saldo: {Saldo:C}";
}
