# Banco Digital

- Conta
- Clientes
- Cartoes
- Transacoes
- Extratos da conta
- Fatura do cartao

### Objetivo do dia (30-09-2025)

- Criar as camadas do projeto

````
Domain/Models/Customer.cs
Application/DTOs/CreateCustomerDto.cs
Infrastructure/Repositories/ICustomerRepository.cs
Api/Controllers/CustomersController.cs
Infrastructure/Repositories/CustomerFileRepository.cs
Data/customers.json
````

- Criar o modelo/DTO/Repository de Cliente/Customer
- Camada de dados em json
- Endpoints: POST de clientes, GetAll, GetById