namespace DigitalBank.Api.Domain.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Adress { get; set; } = string.Empty;

    // Todo: Add birthdate
}