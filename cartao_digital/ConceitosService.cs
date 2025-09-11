public static class ConceitosService
{
    public static List<string> IfDemo()
    {
        var logs = new List<string>();
        int numero = 10;

        if (numero > 5)
        {
            Console.WriteLine("Número é maior que 5");
            logs.Add("Número é maior que 5");
        }

        if (numero % 2 == 0)
        {
            Console.WriteLine("Número é par");
            logs.Add("Número é par");
        }

        if (numero < 0)
        {
            Console.WriteLine("Número é negativo");
            logs.Add("Número é negativo");
        }

        return logs;
    }

    public static List<string> ElseDemo()
    {
        var logs = new List<string>();
        decimal saldo = 10m;
        decimal valorSaque = 50m;

        if (saldo >= valorSaque)
        {
            Console.WriteLine("Saque autorizado");
            logs.Add("Saque autorizado");
        }
        else
        {
            Console.WriteLine("Saldo insuficiente");
            logs.Add("Saldo insuficiente");
        }

        return logs;
    }

    public static List<string> SwitchDemo()
    {
        var logs = new List<string>();
        TipoDeOperacao operacao = TipoDeOperacao.PIX;

        switch (operacao)
        {
            case TipoDeOperacao.PIX:
                Console.WriteLine("Operação via PIX selecionada");
                logs.Add("Operação via PIX selecionada");
                break;
            case TipoDeOperacao.TED:
                Console.WriteLine("Operação via TED selecionada");
                logs.Add("Operação via TED selecionada");
                break;
            default:
                Console.WriteLine("Tipo de operação desconhecida");
                logs.Add("Tipo de operação desconhecida");
                break;
        }

        return logs;

    }

    public enum TipoDeOperacao
    {
        PIX,
        TED
    }

    public static List<string> ForDemo()
    {
        var logs = new List<string>();
        decimal saldo = 0m;

        for (int i = 1; i <= 3; i++)
        {
            saldo += 10m;
            var msg = $"Depósito {i}: saldo atual é {saldo}";
            Console.WriteLine(msg);
            logs.Add(msg);
        }

        return logs;
    }

    public static List<string> ForEachDemo()
    {
        var logs = new List<string>();
        var movimentacoes = new List<decimal> { 100m, -25m, 50m };
        decimal saldo = 0m;

        foreach (var movimentacao in movimentacoes)
        {
            saldo += movimentacao;
            var msg = $"Movimentação de {movimentacao}: saldo atual é {saldo}";
            Console.WriteLine(msg);
            logs.Add(msg);
        }

        return logs;
    }

    public static List<string> DoWhileDemo()
    {
        var logs = new List<string>();
        decimal saldo = 90m;
        decimal tarifa = 30m;
        int contador = 0;

        do
        {
            contador++;
            saldo -= tarifa;
            var msg = $"Tarifa {contador} aplicada. Saldo atual é {saldo}";
            Console.WriteLine(msg);
            logs.Add(msg);
        } while (saldo > 0m);

        var finalMensagem = $"Foram cobradas {contador} tarifas até o saldo zerar.";
        Console.WriteLine(finalMensagem);
        logs.Add(finalMensagem);
        return logs;
    }

    public static List<string> LinqDemo()
    {
        var logs = new List<string>();
        var transacoes = new decimal[] { 100m, 50m, 25m, 10m, 500m, 130m, 400m, 899m, };

        var transacoesAcimaDe99Reais = transacoes
            .Where(v => v > 90m)
            .OrderByDescending(v => v)
            .Take(2)
            .ToList();

        foreach (var transacao in transacoesAcimaDe99Reais)
        {
            var msg = $"Transação acima de 99 reais: {transacao}";
            Console.WriteLine(msg);
            logs.Add(msg);
        }

        return logs;
    }
}