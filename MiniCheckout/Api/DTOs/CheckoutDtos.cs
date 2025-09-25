namespace MiniCheckout.Api.DTOs;

public record AddItemDto(int ProductId, int Quantity);
public record CheckoutRequest(List<AddItemDto> Items);
public record CheckoutLineDto(int ProductId, string ProductName, decimal UnitPrice, int Quantity, decimal TotalPrice);
public record CheckoutResponse(
    List<CheckoutLineDto> Items,
    decimal Total,
    string DiscountStrategy,
    decimal DiscountedTotal,
    string? ExportTxtPath = null,
    string? ExportJsonPath = null);