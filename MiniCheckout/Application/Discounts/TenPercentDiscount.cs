namespace MiniCheckout.Application.Discounts;

public class TenPercentDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price * 0.9m;
}