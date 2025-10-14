using System.Text.Json;
using DigitalBank.Api.Domain.Models;
using Microsoft.Extensions.Hosting;

namespace DigitalBank.Api.Infrastructure.Repositories;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    public AccountRepository(IHostEnvironment env) : base(env, "accounts.json")
    {
    }

    public Account CreateAccount(Account account)
    {
        return WithLock(accountsFromBase =>
        {
            account.Id = NextId(accountsFromBase);
            accountsFromBase.Add(account);
            return account;
        });
    }

    public Account? GetById(int id)
    {
        return ReadOnly(all => all
            .FirstOrDefault(account => account.Id == id));
    }

    public void UpdateBalance(Account account)
    {
        WithLock(accountsFromBase =>
        {
            var index = accountsFromBase.FindIndex(accountFromDb => accountFromDb.Id == account.Id);
            if (index < 0) return;
            accountsFromBase[index] = account;
        });
    }

    private static int NextId(List<Account> accounts)
        => accounts.Count == 0 ? 1 : accounts.Max(accounts => accounts.Id) + 1;
}