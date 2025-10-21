# Aula 8 — Projeto Digital Bank: Kickoff (Parte 1)

## Objetivos
- Subir a Web API com Swagger.
- Cadastrar clientes persistindo em arquivo JSON.
- Validar CPF e expor endpoints de criação e consulta.

## Arquivos a criar/atualizar
- `DigitalBank/Program.cs` (configuração mínima da API e Swagger)
- `DigitalBank/Domain/Models/Customer.cs`
- `DigitalBank/Infrastructure/Interfaces/ICustomerRepository.cs`
- `DigitalBank/Infrastructure/Repositories/CustomerRepository.cs`
- `DigitalBank/Application/DTOs/CreateCustomerDto.cs`
- `DigitalBank/Api/Controllers/CustomerController.cs`
- `DigitalBank/Application/Validation/CpfUtils.cs`
- `DigitalBank/Data/customers.json`

## Dicas
- Validação de CPF: concentrar regra em `CpfUtils` e retornar `400 BadRequest` quando inválido.
- Persistência: repositório baseado em arquivo (`customers.json`).

## Prática
- Endpoints: `POST /api/customers`, `GET /api/customers`, `GET /api/customers/{id}`.
- Testar no Swagger e Insomnia.

---
Navegação: [← Aula 7](Aula-7.md) | [Aula 9 →](Aula-9.md)
