# Aulas 10–13 — Projeto Digital Bank

Documentação do projeto final em quatro partes (clientes, contas, cartões e transações), consolidando prática de camadas, DI e persistência em arquivo JSON.

## Parte 1 — Web API + Clientes
Arquivos:
- `DigitalBank/Program.cs` (Swagger)
- `DigitalBank/Domain/Models/Customer.cs`
- `DigitalBank/Infrastructure/Interfaces/ICustomerRepository.cs`
- `DigitalBank/Infrastructure/Repositories/CustomerRepository.cs`
- `DigitalBank/Application/DTOs/CreateCustomerDto.cs`
- `DigitalBank/Application/Validation/CpfUtils.cs`
- `DigitalBank/Api/Controllers/CustomerController.cs`
- `DigitalBank/Data/customers.json`

## Parte 2 — Conta: Saldo e Depósito
Arquivos:
- `DigitalBank/Domain/Models/Account.cs`
- `DigitalBank/Infrastructure/Interfaces/IAccountRepository.cs`
- `DigitalBank/Infrastructure/Repositories/AccountRepository.cs`
- `DigitalBank/Application/DTOs/DepositDto.cs`
- `DigitalBank/Application/Services/AccountService.cs`
- `DigitalBank/Api/Controllers/AccountController.cs`
- `DigitalBank/Data/accounts.json`

Dicas úteis:
- Encontrar a conta por `id` e atualizar o objeto correspondente.
- No `AccountController`, usar o serviço em vez do repositório diretamente.

```csharp
public AccountsController(AccountService service)
{
    _service = service;
}

// Usos:
_service.CreateForCustomer(customerId);
_service.Deposit(id, dto.Amount);
_service.GetBalance(id);
```

## Parte 3 — Cartões: Débito e Crédito
Arquivos:
- `DigitalBank/Domain/Enums/CardTypeEnum.cs`
- `DigitalBank/Domain/Models/Card.cs`
- `DigitalBank/Infrastructure/Interfaces/ICardRepository.cs`
- `DigitalBank/Infrastructure/Repositories/CardRepository.cs`
- `DigitalBank/Application/DTOs/CreateCreditCardDto.cs`
- `DigitalBank/Application/Services/CardService.cs`
- `DigitalBank/Api/Controllers/CardController.cs`
- `DigitalBank/Data/cards.json`

## Parte 4 — Transações (Débito e Crédito)
Arquivos:
- `DigitalBank/Domain/Enums/TransactionTypeEnum.cs`
- `DigitalBank/Domain/Enums/TransactionStatusEnum.cs`
- `DigitalBank/Domain/Models/Transaction.cs`
- `DigitalBank/Infrastructure/Interfaces/ITransactionRepository.cs`
- `DigitalBank/Infrastructure/Repositories/TransactionRepository.cs`
- `DigitalBank/Application/DTOs/PurchaseDto.cs`
- `DigitalBank/Application/Services/TransactionResult.cs`
- `DigitalBank/Application/Services/TransactionService.cs`
- `DigitalBank/Api/Controllers/TransactionController.cs` (endpoints `/transactions/debit-purchase` e `/transactions/credit-purchase`)
- Atualizar `DigitalBank/Program.cs` para registrar repositório/serviço de transações
- `DigitalBank/Data/transactions.json`

## Parte 5 — Relatorios (extrato da conta e fatura do cartao)

---
Navegação: [← Aula 9](../Aula-9.md)
