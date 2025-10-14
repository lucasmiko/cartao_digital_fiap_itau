using DigitalBank.Api.Domains.Models;
using DigitalBank.Api.Infrastructure.Repositories;

namespace DigitalBank.Api.Infrastructure.Repositories;

public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
{
    public TransactionRepository(IHostEnvironment env) : base(env, "transactions.json")
    {
    }

    public Transaction CreateTransaction(Transaction transaction)
    {
        return WithLock(all =>
        {
            transaction.Id = NextId(all);
            all.Add(transaction);
            return transaction;
        });
    }

    public List<Transaction> GetTransactionsByAccountId(int accountId)
    {
        return ReadOnly(all => all
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.Date)
            .ToList());
    }

    public List<Transaction> GetTransactionsByCardAndMonth(int cardId, int month, int year)
    {
        return ReadOnly(all => all
            .Where(t => t.CardId == cardId)
            .Where(t => t.Date.Month == month && t.Date.Year == year)
            .OrderByDescending(t => t.Date)
            .ToList());
    }

    public List<Transaction> GetTransactionsByRangeDate(int accountId, DateTime? fromDate, DateTime? toDate)
    {
        return ReadOnly(all => all
            .Where(t => t.AccountId == accountId)
            .Where(t => !fromDate.HasValue || t.Date >= fromDate.Value)
            .Where(t => !toDate.HasValue || t.Date <= toDate.Value)
            .OrderByDescending(t => t.Date)
            .ToList());
    }

    private int NextId(List<Transaction> transactions)
    {
        return transactions.Count == 0 ? 1 : transactions.Max(t => t.Id) + 1;
    }
}