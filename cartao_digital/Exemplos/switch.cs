public class Switch
{
    public enum Plano
    {
        Gratuito,
        Standard,
        Premium,
        MaisUmPlano
    }

    string VantagemPlano1(Plano plano)
    {
        switch (plano)
        {
            case Plano.Gratuito:
                return "Acesso limitado a recursos básicos.";
            case Plano.Standard:
                return "Acesso a recursos padrão e suporte por e-mail.";
            case Plano.Premium:
                return "Acesso total a todos os recursos e suporte prioritário.";
            default:
                return "Plano desconhecido. Por favor, escolha um plano válido.";
        }
    }
}