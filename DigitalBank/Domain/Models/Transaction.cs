using DigitalBank.Api.Domains.Enums;

namespace DigitalBank.Api.Domains.Models;

public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TransactionTypeEnum Type { get; set; }
    public decimal Amount { get; set; }
    public TransactionStatusEnum Status { get; set; }
    public int? AccountId { get; set; }
    public int? CardId { get; set; }
}