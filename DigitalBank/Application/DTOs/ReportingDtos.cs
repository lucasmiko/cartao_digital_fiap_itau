using DigitalBank.Api.Domains.Enums;

namespace DigitalBank.Api.Application.DTOs;

public record AccountStatementItem(
    DateTime Date,
    TransactionTypeEnum Type,
    decimal Amount,
    string Status,
    int? CardId
);

public record AccountStatementResponse(
    int AccountId,
    int Month,
    int Year,
    List<AccountStatementItem> Transactions,
    decimal TotalCredits,
    decimal TotalDebits
);

public record CardInvoiceItem(
    DateTime Date,
    decimal Amount,
    string Status
);

public record CardInvoiceResponse(
    int CardId,
    int Month,
    int Year,
    List<CardInvoiceItem> Purchases,
    decimal TotalAmount,
    int? AccountId = null
);