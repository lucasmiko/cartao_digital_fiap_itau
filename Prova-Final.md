# Prova Final — Backend .NET/C# (Projeto DigitalBank)

Tempo sugerido: 60 minutos
Formato: Respostas em arquivo único (ex.: `Respostas-NomeAluno.md`), ou por e-mail (lucasmiko.dev@gmail.com). Use trechos de código quando solicitado.
Recursos: Código do repositório permitido; sem pesquisa externa.

## Parte A — Teoria (5 questões)

1) HTTP/REST — status codes
- Você implementou um depósito em `POST /api/accounts/{id}/deposit`.
  Em cada caso abaixo, qual status code é o mais adequado? Justifique em 1 frase.
  a) Conta inexistente.  b) Valor inválido (<= 0).  c) Depósito bem-sucedido.

2) Controllers e validação
- Em ASP.NET Core, o que `ModelState.IsValid` verifica e onde é configurada a validação neste projeto? Cite um exemplo de DTO com anotações.

3) Injeção de Dependências (DI)
- No `DigitalBank/Program.cs`, os serviços e repositórios foram registrados como `AddSingleton`.
  a) Explique a diferença entre `Singleton`, `Scoped` e `Transient`.
  b) Dê 1 argumento a favor e 1 contra usar `Singleton` para os repositórios que persistem em arquivo JSON.

4) Camadas e responsabilidades
- Diferencie, neste projeto, as responsabilidades de `Controllers`, `Services (Application)` e `Repositories (Infrastructure)`.
  Dê um exemplo rápido de cada camada.

5) Serialização e persistência
- No `RepositoryBase<T>`, cite duas medidas de segurança/robustez adotadas para leitura/escrita de JSON e por que são importantes.

## Parte B — Leitura de Código (4 questões)

6) O que há de errado aqui?

```csharp
// DigitalBank/Application/Services/ReportingService.cs
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
```
- Aponte o problema e escreva a assinatura corrigida.

7) Nome e contrato do endpoint

```csharp
// DigitalBank/Api/Controllers/AccountController.cs
[HttpPost]
public ActionResult<Account> CreateAccont([FromQuery] int customerId) { ... }

[HttpGet("{id:int}/balance")]
public ActionResult<decimal> GetBalance([FromRoute] int id)
{
    var balance = _accountService.GetBalance(id);
    if (balance == null) return NotFound("Conta não encontrada.");
    return Ok(new { accounId = id, balance });
}
```
- Liste 2 problemas de nomenclatura/contrato e proponha correções objetivas (método, rota, payload).

8) Model Binding e validação

```csharp
// DigitalBank/Api/Controllers/CardController.cs
[HttpPost("credit")]
public ActionResult<Card> CreateCreditCard([FromQuery] int accounId, CreateCreditCardDto dto)
{
    if (!ModelState.IsValid) return ValidationProblem(ModelState);
    if (accounId <= 0) return BadRequest("O Id da conta é obrigatório.");
    var card = _cardService.CreateCreditCard(accounId, dto.CreditLimit);
    if (card == null) return NotFound("Ocorreu um erro ao criar o cartão. Tente novamente.");
    return CreatedAtAction(nameof(GetCardsByAccount), new { accountId = accounId }, card);
}
```
- Identifique 2 melhorias/correções relacionadas a binding/validação e explique por quê.

9) Domínio — nomes expressivos

```csharp
// DigitalBank/Domain/Models/Customer.cs
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
}
```
- Aponte o problema no nome da propriedade e comente rapidamente como tratar o impacto em persistência/migração.

## Parte C — Mão na Massa (3 questões)

10) Regra de negócio — transação débito
- Considere: Conta 1 com saldo R$ 200,00, Cartão 10 (débito) ativo e vinculado à conta 1. Uma compra de R$ 180,00 é solicitada.
  a) Qual deve ser o `Status` da `Transaction`?  b) Qual o novo `Balance`?  c) Qual o status code da resposta?
  d) Escreva (em pseudocódigo ou C#) as 3 linhas essenciais que atualizam o saldo e salvam a transação.

11) Filtrar transações por período (C#)
- Implemente o corpo do método a seguir, considerando que `fromDate` e `toDate` são opcionais e o resultado deve ser ordenado por data decrescente.

```csharp
// Assinatura semelhante à do projeto
public List<Transaction> GetTransactionsByRangeDate(int accountId, DateTime? fromDate, DateTime? toDate)
{
    // implemente aqui
}
```

12) Endpoint de relatório (JSON)
- Crie um endpoint `GET /api/accounts/{id}/statement` que receba `month` e `year` via query string e retorne o JSON de `AccountStatementResponse` usando `ReportingService.GetAccountStatement`.
  a) Escreva a assinatura do método no controller e o corpo (tratando 400/404/200).
  b) Dê um exemplo de resposta JSON (campo/valor) com 1 transação de crédito e 1 débito.

---
Entrega: suba um arquivo `Respostas-NomeAluno.md` com as respostas, ou mande no e-mail (lucasmiko.dev@gmail.com). Onde houver código, use bloco ```csharp.

Boa prova!
