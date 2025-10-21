# Aula 9 — Projeto Digital Bank: Cartões (Parte 3)

## Objetivos
- Criar cartões de débito e crédito vinculados à conta.
- Expor endpoints para criação e consulta de cartões.
- Persistir dados em `cards.json` via repositório.

## Arquivos a criar/atualizar
- `DigitalBank/Domain/Enums/CardTypeEnum.cs`
- `DigitalBank/Domain/Models/Card.cs`
- `DigitalBank/Infrastructure/Interfaces/ICardRepository.cs`
- `DigitalBank/Infrastructure/Repositories/CardRepository.cs`
- `DigitalBank/Application/DTOs/CreateCreditCardDto.cs`
- `DigitalBank/Application/Services/CardService.cs`
- `DigitalBank/Api/Controllers/CardController.cs`
- `DigitalBank/Data/cards.json`

## Endpoints
- `POST /api/cards/debit?accountId={id}` — cria cartão de débito para a conta.
- `POST /api/cards/credit?accountId={id}` — cria cartão de crédito (usa `CreateCreditCardDto`).
- `GET  /api/cards?accountId={id}` — lista cartões da conta.

## Prática
- Garantir validações no controller (ex.: `accountId` obrigatório) e mensagens úteis.
- Orquestrar no `CardService` as regras de criação e persistir pelo repositório.
- Testar no Swagger e Insomnia.

---
Navegação: [← Aula 8](Aula-8.md) | [Aulas 10–13 →](DigitalBank/Digital-Bank.md)
