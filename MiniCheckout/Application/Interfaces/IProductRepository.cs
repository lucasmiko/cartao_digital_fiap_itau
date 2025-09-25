using MiniCheckout.Domain;
using MiniCheckout.Application.Interfaces;

namespace MiniCheckout.Application.Interfaces;

public interface IProductRepository
{
    Product? GetById(int id);
    IReadOnlyList<Product> GetAll();
}