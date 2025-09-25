using MiniCheckout.Application.Interfaces;
using MiniCheckout.Application.Discounts;
using MiniCheckout.Domain;

namespace MiniCheckout.Application.Services;

public class CheckoutService
{
    private readonly IProductRepository _productRepository;
    private readonly IDiscountStrategy _discountStrategy;
    private readonly List<(Product product, int quantity)> _items = new();

    public CheckoutService(IProductRepository productRepository, IDiscountStrategy discountStrategy)
    {
        _productRepository = productRepository;
        _discountStrategy = discountStrategy;
    }

    public void AddItem(int productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        var product = _productRepository.GetById(productId) ?? throw new ArgumentException("Product not found.");

        _items.Add((product, quantity));
    }

    public decimal GetTotal()
    {
        decimal subtotal = _items.Sum(item => item.product.Price * item.quantity);
        return _discountStrategy.ApplyDiscount(subtotal);
    }

    public IReadOnlyList<(Product product, int quantity)> GetItems() => _items;
}