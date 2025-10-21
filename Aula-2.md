# Aula 2 — Controle de Fluxo e Coleções

## Objetivos
- Aplicar condicionais (`if/else`, `switch`).
- Usar laços (`for`, `foreach`, `while`).
- Trabalhar com arrays e `List<T>`.
- Usar expressões lambda e LINQ.

## Condicionais

### `if`/`else`
- Regras simples e lineares; boa leitura sequencial.

```csharp
if (!clienteAtivo) return BadRequest("Cliente inativo");
// segue o fluxo
```

### `switch` (expressions)
- Múltiplas opções exclusivas sobre o mesmo alvo (tipo/valor).

```csharp
var taxa = tipoConta switch
{
    "Corrente" => 12.90m,
    "Poupanca" => 0m,
    _ => 5m
};
```

## Laços
- `for`: precisa de índice e controle de início/fim/passo.
- `foreach`: percorre coleção de forma direta.
- `while`: condição aberta (garanta saída).

```csharp
for (int i = 0; i < transacoes.Count; i++) { /* ... */ }

foreach (var t in transacoes) { /* ... */ }

while (!houveConfirmacao) { /* atualizar condição */ }
```

## Listas e Arrays
- Array (`T[]`): tamanho fixo, acesso por índice.
- Lista (`List<T>`): dinâmica, com métodos utilitários.

Docs `List<T>`: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1

## Expressões lambda e LINQ
- Funções anônimas para passar comportamento. Base do LINQ.
- Forma: `param => corpo`.

```csharp
var pares  = numeros.Where(n => n % 2 == 0);
var nomes  = clientes.Select(c => c.Nome);
var existe = transacoes.Any(t => t.Valor > 1000);
var item   = itens.FirstOrDefault(i => i.Id == id);
```

## Prática
- Refatorar cálculo de taxa com `switch` expression.
- Iterar coleções com `foreach` e `for` quando precisar de índice.
- Filtrar e projetar coleções com LINQ (`Where`, `Select`, `Any`, `FirstOrDefault`).

---
Navegação: [← Aula 1](Aula-1.md) | [Aula 3 →](Aula-3.md)
