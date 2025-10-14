# Digital Bank

## Parte 1 — Kickoff: Web API + Clientes
**Arquivos a serem criados**
- `DigitalBank/Program.cs` (configuração mínima da API e Swagger)
- `DigitalBank/Domain/Models/Customer.cs`
- `DigitalBank/Infrastructure/Repositories/ICustomerRepository.cs`
- `DigitalBank/Infrastructure/Repositories/CustomerFileRepository.cs`
- `DigitalBank/Application/DTOs/CreateCustomerDto.cs`
- `DigitalBank/Application/Validation/CpfUtils.cs`
- `DigitalBank/Api/Controllers/CustomerController.cs`
- `DigitalBank/Data/customers.json`

## Parte 2 — Conta: Saldo e Depósito
**Arquivos a serem criados**
- `DigitalBank/Domain/Models/Account.cs`
- `DigitalBank/Infrastructure/Repositories/IAccountRepository.cs`
- `DigitalBank/Application/DTOs/DepositDto.cs`

- `DigitalBank/Infrastructure/Repositories/AccountRepository.cs`
- `DigitalBank/Api/Controllers/AccountController.cs`

- `DigitalBank/Application/Services/AccountService.cs`
- `DigitalBank/Data/accounts.json`

Dicas úteis:

No UploadBalance precisamos encontrar a Conta que pertence ao Id que enviamos como parametro e entao atualizar o objeto que tem esse Id;
No AccountController usaremos uma camada de serviço, então o nosso construtor e nossos endpoints chamarão o serviço e não o repositório diretamente, e teremos endpoints para CreateAccount, GetBalance e  Deposit, por exemplo:

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
**Arquivos a serem criados**
- `DigitalBank/Domain/Enums/CardType.cs`
- `DigitalBank/Domain/Models/Card.cs`
- `DigitalBank/Infrastructure/Repositories/ICardRepository.cs`
- `DigitalBank/Infrastructure/Repositories/CardFileRepository.cs`
- `DigitalBank/Application/DTOs/CreateCreditCardDto.cs`
- `DigitalBank/Application/Services/CardService.cs`
- `DigitalBank/Api/Controllers/CardsController.cs`
- `DigitalBank/Data/cards.json`

## Parte 4 — Transações (Débito e Crédito)
**Arquivos a serem criados/atualizados com os alunos**
- `DigitalBank/Domain/Enums/TransactionType.cs`
- `DigitalBank/Domain/Enums/TransactionStatus.cs`
- `DigitalBank/Domain/Models/Transaction.cs`
- `DigitalBank/Infrastructure/Repositories/ITransactionRepository.cs`
- `DigitalBank/Infrastructure/Repositories/TransactionFileRepository.cs`

- `DigitalBank/Application/DTOs/PurchaseDto.cs`
- `DigitalBank/Application/Services/TransactionResult.cs`
- `DigitalBank/Application/Services/TransactionService.cs`
- `DigitalBank/Api/Controllers/TransactionsController.cs` (endpoints `/transactions/debit` e `/transactions/credit`)
- Atualização em `DigitalBank/Program.cs` para registrar o repositório e serviço de transações
- `DigitalBank/Data/transactions.json`
