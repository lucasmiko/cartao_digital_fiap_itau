public class Lambda
{
    void Test()
    {
        var valores = new List<int> { 1, 2, 3, 4, 5 };
        
        var pares = valores
        .Where(valorDentroDaLista => valorDentroDaLista % 2 == 0)
        .ToList();

        Console.WriteLine(pares);

        /*
            Where - filtra uma coleção com base em uma condição.
            Select - projeta cada elemento de uma coleção em uma nova forma.
            FirsOrDefault - retorna o primeiro elemento de uma coleção ou um valor padrão se a coleção estiver vazia.
            Count - Contatdor de elementos em uma coleção.
            Sum... - soma os valores de uma coleção.
            Any - verifica se algum elemento em uma coleção satisfaz uma condição.
        */
    }
}