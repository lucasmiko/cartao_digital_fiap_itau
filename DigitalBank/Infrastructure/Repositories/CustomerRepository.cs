using System.Text.Json;
using DigitalBank.Api.Domain.Models;
using Microsoft.Extensions.Hosting;

namespace DigitalBank.Api.Infrastructure.Repositories;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(IHostEnvironment env) : base(env, "customers.json")
    {
    }

    public List<Customer> GetAll()
    {
        return ReadOnly(all => all
            .ToList());
    }

    public Customer? GetById(int id)
    {
        return ReadOnly(all => all
            .FirstOrDefault(customer => customer.Id == id));
    }

    public Customer CreateCustomer(Customer customer)
    {
        return WithLock(all =>
        {
            customer.Id = NextId(all);
            all.Add(customer);
            return customer;
        });
    }

    // Método auxiliar para gerar o próximo ID
    private static int NextId(List<Customer> customers)
    {
        return customers.Count == 0 ? 1 : customers.Max(customers => customers.Id) + 1;
    }
}