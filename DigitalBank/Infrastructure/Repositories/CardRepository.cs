using System.Text.Json;
using DigitalBank.Api.Domain.Models;

namespace DigitalBank.Api.Infrastructure.Repositories;

public class CardRepository : RepositoryBase<Card>, ICardRepository
{
    public CardRepository(IHostEnvironment env) : base(env, "cards.json")
    {
    }

    public Card CreateCard(Card card)
    {
        return WithLock(cards =>
        {
            card.Id = NextId(cards);
            cards.Add(card);
            return card;
        });
    }

    public Card? GetById(int id)
    {
        return ReadOnly(all => all
            .FirstOrDefault(card => card.Id == id));
    }

    public List<Card> GetCardsByAccountId(int accountId)
    {
        return ReadOnly(all => all
            .Where(card => card.AccountId == accountId)
            .ToList());
    }

    public void UpdateCard(Card card)
    {
        WithLock(cards =>
        {
            var index = cards.FindIndex(cardFromDb => cardFromDb.Id == card.Id);
            if (index < 0) return;
            cards[index] = card;
        });
    }

    private static int NextId(List<Card> cards)
        => cards.Count == 0 ? 1 : cards.Max(card => card.Id) + 1;
}