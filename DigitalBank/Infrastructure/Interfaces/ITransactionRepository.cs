using DigitalBank.Api.Domains.Models;

namespace DigitalBank.Api.Infrastructure.Repositories;

public interface ITransactionRepository
{
    Transaction CreateTransaction(Transaction transaction);
    List<Transaction> GetTransactionsByAccountId(int accountId);
    List<Transaction> GetTransactionsByRangeDate(int accountId, DateTime? fromDate, DateTime? toDate);
    List<Transaction> GetTransactionsByCardAndMonth(int cardId, int month, int year);
}