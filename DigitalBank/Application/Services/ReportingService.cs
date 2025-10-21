using DigitalBank.Api.Infrastructure.Repositories;
using DigitalBank.Api.Domains.Enums;
using System.Text;
using DigitalBank.Api.Application.DTOs;

namespace DigitalBank.Api.Application.Services;

public class ReportingService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICardRepository _cardRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IHostEnvironment _hostEnvironment;

    public ReportingService(
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

    public CardInvoiceResponse? GetCardInvoice(int cardId, int month, int year)
    {
        var card = _cardRepository.GetById(cardId);
        if (card is null)
            return null;

        var transactions = _transactionRepository
            .GetTransactionsByCardAndMonth(cardId, month, year)
            .Where(transaction => transaction.Type == TransactionTypeEnum.CreditPurchase)
            .OrderBy(transaction => transaction.Date)
            .ToList();

        if (!transactions.Any())
            return new CardInvoiceResponse(cardId, month, year, new List<CardInvoiceItem>(), 0m);

        var items = transactions.Select(transaction => new CardInvoiceItem(
            transaction.Date,
            transaction.Amount,
            transaction.Status.ToString()
        )).ToList();

        decimal totalAmount = transactions
            .Where(transaction => transaction.Status == TransactionStatusEnum.Approved)
            .Sum(transaction => transaction.Amount);

        return new CardInvoiceResponse(cardId, month, year, items, totalAmount);
    }

    public string? GenerateAndSaveAccountStatementReport(int accountId, int month, int year)
    {
        var statement = GetAccountStatement(accountId, month, year);
        if (statement is null)
            return null;

        var fileName = $"extrato-conta-{accountId}-{month}-{year}.txt";
        var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Reportings", fileName);

        var content = GenerateAccountStatementContent(statement);
        File.WriteAllText(filePath, content, Encoding.UTF8);

        return fileName;
    }

    private string GenerateAccountStatementContent(AccountStatementResponse statement)
    {
        var sb = new StringBuilder();
        /*
            Cabeçalho do extrato
            Conta, Periodo, Total de Créditos, Total de Débitos, Data de Geração 

            Verificar se há transações
            Se sim
            Listar transações -> Data, tipo, valor, status...

            gerar items de transacoes, e entao colocar informacoes sobre o cartao, data de cada item

            Resumo do extrato com: 
            Total de Créditos: R$ X,XX
            Total de Débitos: R$ X,XX
            Saldo Final: R$ X,XX
            Data de pagamento... 
        */

        sb.AppendLine("==========================================");
        sb.AppendLine("           EXTRATO DE CONTA");
        sb.AppendLine();
        sb.AppendLine($"Conta: {statement.AccountId}");
        sb.AppendLine($"Período: {statement.Month:00}/{statement.Year}");
        sb.AppendLine($"Data de geração: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        sb.AppendLine();

        if (statement.Transactions.Any())
        {
            sb.AppendLine("TRANSAÇÕES:");
            sb.AppendLine("Data       | Tipo           | Valor        | Status      | Cartão");
            sb.AppendLine("-----------|----------------|--------------|-------------|--------");

            foreach (var item in statement.Transactions)
            {
                var cardInfo = item.CardId.HasValue ? $"Cartão {item.CardId}" : "N/A";
                sb.AppendLine($"{item.Date:dd/MM/yyyy} | {GetTransactionTypeName(item.Type),-14} | {item.Amount,12:C} | {item.Status,-11} | {cardInfo}");
            }
        }
        else
        {
            sb.AppendLine("Nenhuma transação encontrada no período.");
        }

        sb.AppendLine();
        sb.AppendLine("==========================================");
        sb.AppendLine("RESUMO:");
        sb.AppendLine($"Total de Créditos: {statement.TotalCredits:C}");
        sb.AppendLine($"Total de Débitos:  {statement.TotalDebits:C}");
        sb.AppendLine($"Saldo Líquido:     {(statement.TotalCredits - statement.TotalDebits):C}");
        sb.AppendLine("==========================================");

        return sb.ToString();
    }

    private object GetTransactionTypeName(TransactionTypeEnum type)
    {
        return type switch
        {
            TransactionTypeEnum.Deposit => "Depósito",
            TransactionTypeEnum.DebitPurchase => "Compra Débito",
            TransactionTypeEnum.CreditPurchase => "Compra Crédito",
            _ => "Desconhecido"
        };
    }

    public string? GenerateAndSaveCardInvoiceReport(int cardId, int month, int year)
    {
        var invoice = GetCardInvoice(cardId, month, year);
        if (invoice is null)
            return null;

        var fileName = $"fatura-cartao-{cardId}-{month}-{year}.txt";
        var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Reportings", fileName);

        var content = GenerateCardInvoiceContent(invoice);
        File.WriteAllText(filePath, content, Encoding.UTF8);

        return fileName;
    }

    private string? GenerateCardInvoiceContent(CardInvoiceResponse invoice)
    {
        var sb = new StringBuilder();
        sb.AppendLine("==========================================");
        sb.AppendLine("           FATURA DO CARTÃO");
        sb.AppendLine("==========================================");
        sb.AppendLine($"Conta: {invoice.AccountId}");
        sb.AppendLine($"Cartão: {invoice.CardId}");
        sb.AppendLine($"Período: {invoice.Month:00}/{invoice.Year}");
        sb.AppendLine($"Data de geração: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        sb.AppendLine();

        if (invoice.Purchases.Any())
        {
            sb.AppendLine("COMPRAS:");
            sb.AppendLine("Data       | Valor        | Status");
            sb.AppendLine("-----------|--------------|-------------");

            foreach (var item in invoice.Purchases)
            {
                sb.AppendLine($"{item.Date:dd/MM/yyyy} | {item.Amount,12:C} | {item.Status,-11}");
            }
        }
        else
        {
            sb.AppendLine("Nenhuma compra encontrada no período.");
        }

        sb.AppendLine();
        sb.AppendLine("==========================================");
        sb.AppendLine($"VALOR TOTAL DA FATURA: {invoice.TotalAmount:C}");
        sb.AppendLine("==========================================");

        return sb.ToString();
    }
}
