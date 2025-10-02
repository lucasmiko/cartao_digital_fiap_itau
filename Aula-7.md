# Clean Code

## O que é?

- Conjunto de principios que nos ajudam a priorizar: legibilidade, clareza e simplicidade.

### Objetivo

Fazer com que o pós-entrega seja suave e tranquilo.

## Convenções C# / .NET

### Nomes

- Tipos, Props, Metodos e Eventos: PascalCase
    - Customer, Cliente, CreateCard, CardCreated

- Variaveis locais, parametros: camelCase
    - orderId, amount

- Campos privados: camelCase com _
    - _cache, _repository

- Interfaces: PascalCase com o prefixo I
    - IOrderRepository, ICustomer

- bool: comece com Is/Has/Can/Should
    - IsActive, HasErrors, CanDeposit, ShouldRetry

Dica: escolher nomes que descrevem intencao (no que verificam), ao inves de pensar na implementacao
    Ex: bool hasSufficientBlance (melhor) vs. bool valid (vago, nao usar)

- Async: sufixo Async
    - GetByIdAsync, SaveAsync

- Contantes: PascalCase

### Estilo Básico

- Arquivos curtos, uma classe pública por arquivo;
- Usings ordenados;
- var: use quando o tipo esta obvio do lado direito
```csharp
var nomeCompleto = "Joao";
```
- Early return/Guard Clauses: evitar piramides de if
- Comentarios: só quando agregam algo alem do codigo (contexto, decisao, risco). Comentarios devem por quê.

```csharp
// Sem Clean Code
public void Do(Customer c, decimal a) {...}

// Aplicando o Clean Code
public void Deposit(Customer customer, decimal amount) {...}
```

### Funções / Métodos

- Mante-los pequenos: ideal até 15-20 linhas;
- Uma responsabilidade (SRP aplicado a metodos);
- Um nivel de abstracao por bloco: nao misture "o que"com "como"
- Sem efeitos colaterais surpreendentes

```csharp
// Difcil de ler, com os ifs aninhados
public void Transfer(Account from, Account to, decimal amount)
{
    if (from != null && to != null)
    {
        if (amount > 0)
        {
            if (from.Balance >= amount)
            {
                from.Balance -= amount;
                to.Balance += amount;
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("No balance");
            }
        }
    }
}

// Depois: aplicando Guard e Clean Code
public void Transfer(Account from, Account to, decimal amount)
{
    if (from is null) throw new ArgumentNullException(nameof(from));
    if (to   is null) throw new ArgumentNullException(nameof(to));
    if (amount <= 0)  throw new ArgumentOutOfRangeException(nameof(amount));

    if (!HasSufficientFunds(from, amount))
        throw new InvalidOperationException("Insufficient funds.");

    Debit(from, amount);
    Credit(to, amount);
}

private static bool HasSufficientFunds(Account account, decimal amount)
    => account.Balance >= amount;

private static void Debit(Account account, decimal amount) => account.Balance -= amount;
private static void Credit(Account account, decimal amount) => account.Balance += amount;

```

## Erros, nulos e exceções

### Nulos em C#

- Null-conditional: customer?.Name
- Coalescência: name ?? "Nome Desconhecido"
- Pattern matching: if (obj is Customer customer) {...}

### Exceptions limpas

- Lance exceções claras e nos padroes corretos.
- Nao use excecoes para o fluxo normal: nao usamos uma checagem de existencia
- Mensagens de erro que ajudam quem vai corrigir: o que deu errado + dado relevante

### Tratamentos

- Capturar o erro onde consegue agir.
- Em ASP.NET Core, centralize com middleware de excecoes.

## Estrutura e Leitura fluida

- Lei de Deméter (nao falar com estranhos)

```csharp
// Sem aplicar essa lei
order.Customer.Address.City.ToUpper();

// Melhor: exponha uma operacao de alto nivel
order.GetCustomerCityUpper();
```

- Niveis de abstracao
    - Metodos autam em um nivel
    - Quebre em metodos com nomes que contam a historia na ordem de leitura

- LINQ com legibilidade

    - Escolher nomes claros nas nossas expressoes lamba e quebrar em variaveis intermediarias quando a logica ficar densa

```csharp
// Denso
var total = items.Where(i => i.Q > 0).Sum(i => i.P * i.Q);

// Mais legivel
var validItems = items.Where(item => item.Quantity > 0);
var total = validItems.Sum(item => item.Price * item.Quantity);
```

- Comentarios uteis (o por quê)

```csharp
// Usei o dicionario pela necessecidade de lookup0(1) em rotas especificas/criticas
var cache = new Dictionary<string, Product>();
```

Exercicio - Refatorar aplicando: nomes melhores, guard clauses, extração de métodos e mensagens de erro claras.

```csharp
public decimal C(Account a, List<Tx> t)
{
    decimal s = 0;
    if (a != null && t != null)
    {
        foreach (var x in t)
        {
            if (x.Type == 1) s += x.Amount;
            else if (x.Type == 2) s -= x.Amount;
        }
    }
    return s;
}

```

Resolucao

```csharp
public decimal CalculateBalance(Account account, IEnumerable<Tx> transactions)
{
    if (account is null)
        throw new ArgumentNullException(nameof(account));
    if (transactions is null)
        throw new ArgumentNullException(nameof(transactions));

    return transactions.Sum(Calc);

    static decimal Calc(Tx tx) => tx.Type switch
    {
        TxType.Credit => tx.Amount,
        TxType.Debit => -tx.Amount,
        _ => throw new InvalidOperationExcepetion();
    };
}

public enum TxType {Credit = 1, Debit = 2};
public record Tx(TxType Type, decimal Amount)
```

## Clean Code - Principios principais

- KISS: se tem duas solucoes para um problema, devemos escolher a mais simples;
- DRY: extrair duplicacoes para um unico lugar apropriado;
- YAGNI: nao condigique para o futuro, eliminar recursos mortos;
- Lei de Deméter;
- SLAP: nivel de abstracao por metodo
- Boy Scout Rule: sempre deixe algo um pouco melhor

### Checklist para as nossas pull-requests

1. **Nomes dizem a intenção?** (Is/Has/Should para bool; Async sufixo; interfaces com I)
2. **Métodos pequenos e uma responsabilidade?**
3. **Sem duplicação óbvia?** 
4. **Fluxo claro e guard clauses para erros?**
5. **Sem acoplamento desnecessário?** (Lei de Deméter)
6. **Tratamento de nulos e exceções adequado?** (mensagens úteis, tipos certos)
7. **Comentários explicam “por quê”, não “o quê”?**
8. **Testabilidade**: código permite teste fácil?
9. **Simplicidade**: evitou over-engineering?
10. **Consistência**: segue padrão do repositório (pastas, usings, `var`, formatação)?


# Banco Digital

- Conta
- Clientes
- Cartoes
- Transacoes
- Extratos da conta
- Fatura do cartao

### Objetivo do dia (30-09-2025)

- Criar as camadas do projeto

````
Domain/Models/Customer.cs
Application/DTOs/CreateCustomerDto.cs
Infrastructure/Repositories/ICustomerRepository.cs
Api/Controllers/CustomersController.cs
Infrastructure/Repositories/CustomerFileRepository.cs
Data/customers.json
````

- Criar o modelo/DTO/Repository de Cliente/Customer
- Camada de dados em json
- Endpoints: POST de clientes, GetAll, GetById