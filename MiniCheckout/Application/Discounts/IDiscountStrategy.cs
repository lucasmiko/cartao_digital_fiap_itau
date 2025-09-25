namespace MiniCheckout.Application.Discounts;

public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal price);
}