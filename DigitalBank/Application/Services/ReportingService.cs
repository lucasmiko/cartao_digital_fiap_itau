using DigitalBank.Api.Infrastructure.Repositories;
using DigitalBank.Api.Domains.Enums;

namespace DigitalBank.Api.Application.Services;

public class ResportingService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICardRepository _cardRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IHostEnvironment _hostEnvironment;

    public ResportingService(
        IAccountRepository accountRepository,
        ICardRepository cardRepository,
        ITransactionRepository transactionRepository,
        IHostEnvironment hostEnvironment)
    {
        _accountRepository = accountRepository;
        _cardRepository = cardRepository;
        _transactionRepository = transactionRepository;
        _hostEnvironment = hostEnvironment;
    }

    public AccountStatementResponse? GetAccountStatement(int accountId, int month, int year)
    {
        var account = _accountRepository.GetById(accountId);
        if (account is null)
            return null;

        var from = new DateTime(year, month, 1);
        var to = from.AddMonths(1).AddDays(-1);

        var transactions = _transactionRepository
            .GetTransactionsByRangeDate(accountId, from, to)
            .OrderBy(transaction => transaction.Date)
            .ToList();

        var items = transactions.Select(transaction => new AccountStatementItem(
            transaction.Date,
            transaction.Type,
            transaction.Amount,
            transaction.Status.ToString(),
            transaction.CardId
        )).ToList();

        decimal totalCredits = transactions
            .Where(transaction => transaction.Type == TransactionTypeEnum.Deposit)
            .Sum(transaction => transaction.Amount);

        var totalDebits = transactions
            .Where(transaction => transaction.Type == TransactionTypeEnum.DebitPurchase)
            .Sum(transaction => transaction.Amount);
        return new AccountStatementResponse(accountId, month, year, items, totalCredits, totalDebits);
    }
}
