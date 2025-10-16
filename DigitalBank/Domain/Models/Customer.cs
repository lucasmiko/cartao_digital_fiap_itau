namespace DigitalBank.Api.Domain.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;

    // Todo: Add birthdate
}