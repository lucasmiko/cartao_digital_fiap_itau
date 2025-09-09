# Encontro 2 — Controle de Fluxo e Estruturas

## Condicionais

### `if` / `else`
- Regras simples e lineares.
- Use quando a leitura sequencial faz sentido.

```csharp
if (!clienteAtivo) return BadRequest("Cliente inativo");
// segue o fluxo
```

### `switch`
- Múltiplas opções **exclusivas** sobre o mesmo alvo (tipo/valor).

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
- `foreach`: percorrer coleção de forma direta.
- `while`: condição aberta (garanta saída).

```csharp
for (int i = 0; i < transacoes.Count; i++) { /* ... */ }

foreach (var t in transacoes) { /* ... */ }

while (!houveConfirmacao) { /* atualizar condição */ }
```

## Listas e Arrays

- **Array (`T[]`)**: tamanho **fixo**, acesso por índice.
- **List (`List<T>`)**: **dinâmica**, métodos utilitários.

Docs `List<T>`: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-9.0

## Expressões Lambda

- Funções anônimas para passar comportamento. Base do LINQ.
- Forma: `param => corpo`

```csharp
var pares  = numeros.Where(n => n % 2 == 0);
var nomes  = clientes.Select(c => c.Nome);
var existe = transacoes.Any(t => t.Valor > 1000);
var item   = itens.FirstOrDefault(i => i.Id == id);
```
