using DigitalBank.Api.Domains.Models;

namespace DigitalBank.Api.Application.Services;

public class TransactionResult
{
    public bool Success { get; set; }
    public bool NotFound { get; set; }
    public string Message { get; set; } = string.Empty;
    public Transaction? Transaction { get; set; }
}