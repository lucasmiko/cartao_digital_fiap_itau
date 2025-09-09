public class Listas
{
    void Test()
    {
        var notasPreDefinidas = new List<int> { 10, 9, 8, 7, 6 };

        var notasVazia = new List<int>();
        Console.WriteLine(notasPreDefinidas[0]);
        notasVazia.Add(10); // notasVazia = new List<int> { 10 };
        Console.WriteLine(notasVazia[0]);
    }
}