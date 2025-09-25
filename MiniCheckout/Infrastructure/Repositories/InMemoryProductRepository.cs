using MiniCheckout.Application.Interfaces;
using MiniCheckout.Domain;

namespace MiniCheckout.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product(1, "Notebook", 5000m),
        new Product(2, "Smartphone", 3000m),
        new Product(3, "Tablet", 2000m),
        new Product(4, "Smartwatch", 1000m),
        new Product(5, "Headphones", 500m)
    };

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public IReadOnlyList<Product> GetAll() => _products;
}