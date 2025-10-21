# Aula 7 — Clean Code no C#

## Objetivos
- Escrever código legível, claro e simples.
- Facilitar manutenção, testes e evolução do sistema.

## Convenções C#/.NET — Nomes
- Tipos, propriedades, métodos e eventos: PascalCase (ex.: `CreateCard`).
- Variáveis locais, parâmetros: camelCase (ex.: `orderId`).
- Campos privados: camelCase com `_` (ex.: `_repository`).
- Interfaces: prefixo `I` (ex.: `IOrderRepository`).
- Booleanos: comece com Is/Has/Can/Should (ex.: `HasErrors`).
- Assíncronos: sufixo `Async` (ex.: `GetByIdAsync`).
- Constantes: PascalCase.

> Prefira nomes que expressem intenção (ex.: `HasSufficientBalance`).

## Estilo básico
- Arquivos curtos; uma classe pública por arquivo.
- `var` quando o tipo está evidente no lado direito.

```csharp
var nome = "João";
```

- Early return/Guard Clauses para evitar pirâmides de `if`.
- Comentários apenas quando agregam contexto/decisão/risco (explique o porquê).

```csharp
// Sem Clean Code
public void Do(Customer c, decimal a) { /* ... */ }

// Melhor
public void Deposit(Customer customer, decimal amount) { /* ... */ }
```

## Funções/Métodos
- Pequenos (ideal até 15–20 linhas) e com uma responsabilidade.
- Um nível de abstração por bloco (não misture “o quê” com “como”).

```csharp
// Antes: aninhado e difícil de ler
public void Transfer(Account from, Account to, decimal amount)
{
    if (from != null && to != null)
    {
        if (amount > 0)
        {
            if (from.Balance >= amount)
            {
                from.Balance -= amount;
                to.Balance += amount;
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("No balance");
            }
        }
    }
}

// Depois: guard clauses + extração de métodos
public void Transfer(Account from, Account to, decimal amount)
{
    if (from is null) throw new ArgumentNullException(nameof(from));
    if (to   is null) throw new ArgumentNullException(nameof(to));
    if (amount <= 0)  throw new ArgumentOutOfRangeException(nameof(amount));

    if (!HasSufficientFunds(from, amount))
        throw new InvalidOperationException("Insufficient funds.");

    Debit(from, amount);
    Credit(to, amount);
}

private static bool HasSufficientFunds(Account account, decimal amount)
    => account.Balance >= amount;

private static void Debit(Account account, decimal amount) => account.Balance -= amount;
private static void Credit(Account account, decimal amount) => account.Balance += amount;
```

## Erros, nulos e exceções
- Null-conditional: `customer?.Name`.
- Coalescência: `name ?? "Nome Desconhecido"`.
- Pattern matching: `if (obj is Customer customer) { ... }`.
- Exceções limpas: tipos corretos e mensagens úteis; não use exceções no fluxo normal.
- Trate onde puder agir; no ASP.NET Core, centralize com middleware quando possível.

## Estrutura e leitura fluida
- Lei de Deméter (não falar com “estranhos”).

```csharp
// Sem aplicar a lei
order.Customer.Address.City.ToUpper();

// Melhor: exponha operação de alto nível
order.GetCustomerCityUpper();
```

- Níveis de abstração: métodos atuam em um mesmo nível; quebre em métodos nomeados.
- LINQ legível: nomes descritivos e variáveis intermediárias quando necessário.

```csharp
// Denso
var total = items.Where(i => i.Q > 0).Sum(i => i.P * i.Q);

// Mais legível
var validItems = items.Where(item => item.Quantity > 0);
var total = validItems.Sum(item => item.Price * item.Quantity);
```

```csharp
// Comentários úteis (o porquê)
// Usei dicionário pela necessidade de lookup O(1) em rotas críticas
var cache = new Dictionary<string, Product>();
```

## Exercício
Refatore o método abaixo aplicando: nomes melhores, guard clauses, extração de métodos e mensagens de erro claras.

```csharp
public decimal C(Account a, List<Tx> t)
{
    decimal s = 0;
    if (a != null && t != null)
    {
        foreach (var x in t)
        {
            if (x.Type == 1) s += x.Amount;
            else if (x.Type == 2) s -= x.Amount;
        }
    }
    return s;
}
```

### Possível solução
```csharp
public decimal CalculateBalance(Account account, IEnumerable<Tx> transactions)
{
    if (account is null) throw new ArgumentNullException(nameof(account));
    if (transactions is null) throw new ArgumentNullException(nameof(transactions));

    return transactions.Sum(Calc);

    static decimal Calc(Tx tx) => tx.Type switch
    {
        TxType.Credit => tx.Amount,
        TxType.Debit  => -tx.Amount,
        _ => throw new InvalidOperationException()
    };
}

public enum TxType { Credit = 1, Debit = 2 }
public record Tx(TxType Type, decimal Amount);
```

## Princípios principais
- KISS: prefira a solução mais simples que resolve o problema.
- DRY: extraia duplicações para um único lugar apropriado.
- YAGNI: evite programar para um futuro incerto; elimine código morto.
- Lei de Deméter; SLAP (um nível de abstração por método).
- Boy Scout Rule: sempre deixe algo um pouco melhor.

### Checklist de PRs
1) Nomes dizem a intenção? (`Is/Has/Should`, sufixo `Async`, `I` em interfaces)
2) Métodos pequenos e com uma responsabilidade?
3) Sem duplicação óbvia?
4) Fluxo claro e guard clauses para erros?
5) Sem acoplamento desnecessário? (Lei de Deméter)
6) Nulos e exceções tratados corretamente?
7) Comentários explicam “por quê”, não “o quê”?
8) Testável? Facilita teste unitário?
9) Simplicidade: sem over-engineering?
10) Consistência com o repositório (pastas/usings/`var`/formatação)?

---
Navegação: [← Aula 6](Aula-6.md) | [Aula 8 →](Aula-8.md)
