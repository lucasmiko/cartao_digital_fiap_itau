namespace MiniCheckout.Application.Discounts;

public class NoDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price;
}