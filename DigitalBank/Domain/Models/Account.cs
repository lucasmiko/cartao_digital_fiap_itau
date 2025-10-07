using DigitalBank.Api.Domain.Models;

public class Account
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Balance { get; set; } = 0m;
}