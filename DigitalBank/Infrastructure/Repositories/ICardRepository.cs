using DigitalBank.Api.Domain.Models;

namespace DigitalBank.Api.Infrastructure.Repositories;

public interface ICardRepository
{
    Card CreateCard(Card card);
    Card? GetById(int id);
    List<Card> GetCardsByAccountId(int accountId);
    void UpdateCard(Card card);
}