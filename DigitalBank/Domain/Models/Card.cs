using DigitalBank.Api.Domain.Enums;

namespace DigitalBank.Api.Domain.Models;

public class Card
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public CardTypeEnum CardType { get; set; }
    public decimal? CreditLimit { get; set; }
    public decimal AvailableCredit { get; set; }
    public bool IsActive { get; set; } = true;
}