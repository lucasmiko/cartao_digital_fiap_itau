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
}