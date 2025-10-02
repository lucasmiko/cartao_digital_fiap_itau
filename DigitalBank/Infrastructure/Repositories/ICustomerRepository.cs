using DigitalBank.Api.Domain.Models;

namespace DigitalBank.Api.Infrastructure.Repositories;

public interface ICustomerRepository
{
    Customer CreateCustomer(Customer customer);
    List<Customer> GetAll();
    Customer? GetById(int id);
}