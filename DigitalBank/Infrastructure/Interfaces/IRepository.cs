namespace DigitalBank.Api.Infrastructure.Repositories;

public interface IRepository<T>
{
    T Create(T type);
    List<T> GetAll();
    T? GetById(int id);
}