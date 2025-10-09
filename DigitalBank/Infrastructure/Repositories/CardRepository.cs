using System.Text.Json;
using DigitalBank.Api.Domain.Models;

namespace DigitalBank.Api.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly string _filePath;
    private static readonly object _fileLock = new();
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    public CardRepository(IHostEnvironment env)
    {
        var dataDirectory = Path.Combine(env.ContentRootPath, "Data");
        Directory.CreateDirectory(dataDirectory);

        _filePath = Path.Combine(dataDirectory, "cards.json");
        EnsureFileExistsAndValid();
    }

    public Card CreateCard(Card card)
    {
        lock (_fileLock)
        {
            var cards = LoadUnlocked();
            card.Id = NextId(cards);
            cards.Add(card);
            SaveUnlocked(cards);
            return card;
        }
    }

    public Card? GetById(int id)
    {
        lock (_fileLock)
        {
            return LoadUnlocked().FirstOrDefault(card => card.Id == id);
        }
    }

    public List<Card> GetCardsByAccountId(int accountId)
    {
        lock (_fileLock)
        {
            return LoadUnlocked().Where(card => card.AccountId == accountId).ToList();
        }
    }

    public void UpdateCard(Card card)
    {
        lock (_fileLock)
        {
            var cards = LoadUnlocked();
            var index = cards.FindIndex(cardFromDb => cardFromDb.Id == card.Id);
            if (index < 0) return;
            cards[index] = card;
            SaveUnlocked(cards);
        }
    }

    /* Métodos auxiliares para persistência em arquivo JSON */
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

    private List<Card> LoadUnlocked()
    {
        try
        {
            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json)) return new List<Card>();
            return JsonSerializer.Deserialize<List<Card>>(json, _jsonOptions) ?? new List<Card>();
        }
        catch
        {
            File.WriteAllText(_filePath, "[]");
            return new List<Card>();
        }
    }

    private void SaveUnlocked(List<Card> cards)
    {
        var json = JsonSerializer.Serialize(cards, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }

    private static int NextId(List<Card> cards)
        => cards.Count == 0 ? 1 : cards.Max(card => card.Id) + 1;
}