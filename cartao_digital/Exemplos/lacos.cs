public class Lacos
{
    void Test()
    {
        var lista = new List<int> { 3, 4, 5, 6, 7, 8, 9 };

        for (int i = 0; i < 10; i++)
        {
            lista.Add(i);
        }

        foreach (var item in lista)
        {
            Console.WriteLine(item);
        }

        int tentativas = 0;
        var random = new Random();
        do
        {
            tentativas++;
        } while (random.Next(0, 10) == 5);
    }
}