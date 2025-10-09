using DigitalBank.Api.Domain.Models;

namespace DigitalBank.Api.Infrastructure.Repositories;

public interface IAccountRepository
{
    Account CreateAccount(Account account);
    Account? GetById(int id);
    void UpdateBalance(Account account);
}