using System.Text.Json;
using DigitalBank.Api.Domain.Models;
using Microsoft.Extensions.Hosting;

namespace DigitalBank.Api.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly string _filePath;
    private static readonly object _fileLock = new();
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    public AccountRepository(IHostEnvironment env)
    {
        var dataDirectory = Path.Combine(env.ContentRootPath, "Data");
        Directory.CreateDirectory(dataDirectory);

        _filePath = Path.Combine(dataDirectory, "accounts.json");
        EnsureFileExistsAndValid();
    }

    public Account CreateAccount(Account account)
    {
        lock (_fileLock)
        {
            var accounts = LoadUnlocked();
            account.Id = NextId(accounts);
            accounts.Add(account);
            SaveUnlocked(accounts);
            return account;
        }
    }

    public Account? GetById(int id)
    {
        lock (_fileLock)
        {
            return LoadUnlocked().FirstOrDefault(account => account.Id == id);
        }
    }

    public void UpdateBalance(Account account)
    {
        lock (_fileLock)
        {
            var accounts = LoadUnlocked();
            var index = accounts.FindIndex(accountFromDb => accountFromDb.Id == account.Id);
            if (index < 0) return;
            accounts[index] = account;
            SaveUnlocked(accounts);
        }
    }

    // Métodos auxiliares para persistência em arquivo JSON

    private void EnsureFileExistsAndValid()
    {
        try
        {
            if (!File.Exists(_filePath) || string.IsNullOrWhiteSpace(File.ReadAllText(_filePath)))
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

    private List<Account> LoadUnlocked()
    {
        try
        {
            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json)) return new List<Account>();
            return JsonSerializer.Deserialize<List<Account>>(json, _jsonOptions) ?? new List<Account>();
        }
        catch
        {
            File.WriteAllText(_filePath, "[]");
            return new();
        }
    }

    private void SaveUnlocked(List<Account> accounts)
    {
        var json = JsonSerializer.Serialize(accounts, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }

    private static int NextId(List<Account> accounts)
        => accounts.Count == 0 ? 1 : accounts.Max(accounts => accounts.Id) + 1;
}