using DigitalBank.Api.Infrastructure.Repositories;

namespace DigitalBank.Api.Application.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICustomerRepository _customerRepository;

    public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository)
    {
        _accountRepository = accountRepository;
        _customerRepository = customerRepository;
    }

    public Account? CreateForCustomer(int customerId)
    {
        var customer = _customerRepository.GetById(customerId);
        if (customer is null) return null;

        var account = new Account
        {
            CustomerId = customerId,
            Balance = 0m
        };

        return _accountRepository.CreateAccount(account);
    }

    public Account? Deposit(int accountId, decimal amount)
    {
        if (amount <= 0m) return null;

        var account = _accountRepository.GetById(accountId);
        if (account is null) return null;

        // account.Balance = account.Balance + amount;
        account.Balance += amount; // Seria a mesma coisa que account.Balance = account.Balance + amount
        _accountRepository.UpdateBalance(account);
        return account;
    }

    public decimal? GetBalance(int accountId)
    {
        var account = _accountRepository.GetById(accountId);
        if (account is null) return null;

        return account.Balance;
    }
}