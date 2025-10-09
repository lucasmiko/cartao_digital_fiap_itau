using DigitalBank.Api.Domain.Enums;
using DigitalBank.Api.Domain.Models;
using DigitalBank.Api.Infrastructure.Repositories;

namespace DigitalBank.Api.Application.Services;

public class CardService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICardRepository _cardRepository;

    public CardService(
        IAccountRepository accountRepository,
        ICardRepository cardRepository)
    {
        _accountRepository = accountRepository;
        _cardRepository = cardRepository;
    }

    public Card? CreateDebitCard(int accountId)
    {
        var account = _accountRepository.GetById(accountId);
        if (account is null) return null;

        var debitCard = new Card
        {
            AccountId = accountId,
            CardType = CardTypeEnum.Debit,
            CreditLimit = null,
            AvailableCredit = 0,
            IsActive = true
        };

        return _cardRepository.CreateCard(debitCard);
    }

    public Card? CreateCreditCard(int accountId, decimal creditLimit)
    {
        var account = _accountRepository.GetById(accountId);
        if (account is null) return null;

        var creditCard = new Card
        {
            AccountId = accountId,
            CardType = CardTypeEnum.Credit,
            CreditLimit = creditLimit,
            AvailableCredit = creditLimit,
            IsActive = true
        };

        return _cardRepository.CreateCard(creditCard);
    }

    public List<Card>? GetCardsByAccount(int accountId)
    {
        var account = _accountRepository.GetById(accountId);
        if (account is null) return null;

        return _cardRepository.GetCardsByAccountId(accountId);
    }
}