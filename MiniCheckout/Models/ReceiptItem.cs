namespace MiniCheckout.Models;

public record ReceiptItem(
    string Name,
    decimal UnitPrice,
    int Quantity,
    decimal LineTotal
);