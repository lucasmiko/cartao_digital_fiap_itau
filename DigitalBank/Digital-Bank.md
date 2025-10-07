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
- `DigitalBank/Infrastructure/Repositories/AccountFileRepository.cs`
- `DigitalBank/Application/DTOs/DepositDto.cs`
- `DigitalBank/Application/Services/AccountService.cs`
- `DigitalBank/Api/Controllers/AccountController.cs`
- `DigitalBank/Data/accounts.json`

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
