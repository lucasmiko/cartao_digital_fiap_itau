using DigitalBank.Api.Infrastructure.Repositories;
using DigitalBank.Api.Domains.Enums;
using DigitalBank.Api.Domains.Models;

namespace DigitalBank.Api.Application.Services;

public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ICardRepository _cardRepository;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        ICardRepository cardRepository
    )
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _cardRepository = cardRepository;
    }

    public TransactionResult ProcessDebitPurchase(int accountId, int cardId, decimal amount)
    {
        var validateResult = ValidateTransaction(accountId, cardId, amount, CardTypeEnum.Debit);

        if (validateResult is not null)
            return validateResult;

        var account = _accountRepository.GetById(accountId);

        var transaction = new Transaction
        {
            AccountId = accountId,
            CardId = cardId,
            Amount = amount,
            Date = DateTime.UtcNow,
            Type = TransactionTypeEnum.DebitPurchase
        };

        if (account?.Balance >= amount)
        {
            account.Balance -= amount;
            _accountRepository.UpdateBalance(account);

            transaction.Status = TransactionStatusEnum.Approved;
            transaction = _transactionRepository.CreateTransaction(transaction);

            return new TransactionResult
            {
                Success = true,
                Transaction = transaction,
                Message = "Compra realizada com sucesso."
            };
        }

        transaction.Status = TransactionStatusEnum.Blocked;
        transaction = _transactionRepository.CreateTransaction(transaction);

        return new TransactionResult
        {
            Success = false,
            Transaction = transaction,
            Message = "Saldo insuficiente para realizar a compra."
        };
    }

    public TransactionResult ProcessCreditPurchase(int accountId, int cardId, decimal amount)
    {
        var validateResult = ValidateTransaction(accountId, cardId, amount, CardTypeEnum.Credit);

        if (validateResult is not null)
            return validateResult;

        var card = _cardRepository.GetById(cardId);

        var transaction = new Transaction
        {
            AccountId = accountId,
            CardId = cardId,
            Amount = amount,
            Date = DateTime.UtcNow,
            Type = TransactionTypeEnum.CreditPurchase
        };

        if (card?.AvailableCredit >= amount)
        {
            card.AvailableCredit -= amount;
            _cardRepository.UpdateCard(card);

            transaction.Status = TransactionStatusEnum.Approved;
            transaction = _transactionRepository.CreateTransaction(transaction);

            return new TransactionResult
            {
                Success = true,
                Transaction = transaction,
                Message = "Compra realizada com sucesso."
            };
        }

        transaction.Status = TransactionStatusEnum.Blocked;
        transaction = _transactionRepository.CreateTransaction(transaction);

        return new TransactionResult
        {
            Success = false,
            Transaction = transaction,
            Message = "Limite indisponível para realizar a compra."
        };
    }

    private TransactionResult? ValidateTransaction(int accountId, int cardId, decimal amount, CardTypeEnum cardType)
    {
        if (amount <= 0)
        {
            return new TransactionResult
            {
                Success = false,
                Message = "O valor da transação deve ser maior que zero."
            };
        }

        var account = _accountRepository.GetById(accountId);
        var card = _cardRepository.GetById(cardId);

        if (account is null || card is null || card.AccountId != accountId)
        {
            return new TransactionResult
            {
                Success = false,
                NotFound = true,
                Message = "Conta ou cartão não encontrado."
            };
        }

        if (!card.IsActive)
        {
            return new TransactionResult
            {
                Success = false,
                Message = "O cartão está inativo."
            };
        }

        if (card.CardType != cardType)
        {
            var cardTypeName = cardType == CardTypeEnum.Debit ? "débito" : "crédito";

            /*
            if (cardType == CardTypeEnum.Debit)
            {
                cardTypeName = "débito";
            }
            else
            {
                cardTypeName = "crédito";
            }
            */

            return new TransactionResult
            {
                Success = false,
                Message = $"O cartão não é do tipo {cardTypeName}."
            };
        }

        return null;
    }
}