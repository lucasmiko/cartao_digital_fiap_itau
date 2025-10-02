using System.Text.Json;
using DigitalBank.Api.Domain.Models;
using Microsoft.Extensions.Hosting;

namespace DigitalBank.Api.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _filePath;

    // O lock é como um cadeado, que garante que apenas uma thread acesse o arquivo por vez
    private static readonly object _fileLock = new();

    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    public CustomerRepository(IHostEnvironment env)
    {
        var dataDirectory = Path.Combine(env.ContentRootPath, "Data");
        Directory.CreateDirectory(dataDirectory);

        _filePath = Path.Combine(dataDirectory, "customers.json");
        EnsureFileExistsAndValid();
    }

    public List<Customer> GetAll()
    {
        lock (_fileLock)
        {
            return LoadUnlocked().ToList();
        }
    }

    public Customer? GetById(int id)
    {
        lock (_fileLock)
        {
            return LoadUnlocked().FirstOrDefault(customer => customer.Id == id);
        }
    }

    public Customer CreateCustomer(Customer customer)
    {
        lock (_fileLock)
        {
            var customers = LoadUnlocked();
            customer.Id = NextId(customers);
            customers.Add(customer);
            SaveUnlocked(customers);
            return customer;
        }
    }


    // Métodos auxiliares para persistência em arquivo JSON
    private List<Customer> LoadUnlocked()
    {
        try
        {
            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Customer>();
            }

            return JsonSerializer.Deserialize<List<Customer>>(json, _jsonOptions) ?? new List<Customer>();
        }
        catch
        {
            File.WriteAllText(_filePath, "[]");
            return new();
        }
    }

    private void SaveUnlocked(List<Customer> customers)
    {
        var json = JsonSerializer.Serialize(customers, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }

    private static int NextId(List<Customer> customers)
    {
        return customers.Count == 0 ? 1 : customers.Max(customers => customers.Id) + 1;
    }

    private void EnsureFileExistsAndValid()
    {
        try
        {
            if (!File.Exists(_filePath) || new FileInfo(_filePath).Length == 0)
            {
                File.WriteAllText(_filePath, "[]");
                return;
            }

            var text = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(text))
            {
                File.WriteAllText(_filePath, "[]");
                return;
            }

            using var _ = JsonDocument.Parse(text);
        }
        catch
        {
            File.WriteAllText(_filePath, "[]");
        }
    }
}