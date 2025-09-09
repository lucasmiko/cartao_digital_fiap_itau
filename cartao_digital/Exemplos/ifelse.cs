public class Idade
{
    string ClassificarIdade(int idade)
    {
        if (idade < 0)
        {
            return "Idade inválida";
        }
        else if (idade <= 12)
        {
            return "Criança";
        }
        else if (idade <= 19)
        {
            return "Adolescente";
        }
        else if (idade <= 64)
        {
            return "Adulto";
        }
        else
        {
            return "Idoso";
        }
    }

    string ClassificarIdade2(int idade)
    {
        if (idade < 0) return "Idade inválida";
        if (idade <= 12) return "Criança";
        if (idade <= 19) return "Adolescente";
        if (idade <= 64) return "Adulto";
        return "Idoso";
    }

    string ClassificarIdade3(int idade) =>
        idade < 0 ? "Idade inválida" :
        idade <= 12 ? "Criança" :
        idade <= 19 ? "Adolescente" :
        idade <= 64 ? "Adulto" :
        "Idoso";
        // ? : é o operador condicional ternário
}