public static class MathUtils
{
    public static double Somar(double a, double b, double c)
    {
        return a + b + c;
    }

    public static double Subtrair(double a, double b)
    {
        return a - b;
    }

    public static double Multiplicar(double a, double b)
    {
        return a * b;
    }

    public static double Dividir(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("O divisor não pode ser zero.");
        }
        return a / b;
    }
}