# Aula 4 — Persistência de Dados, EF Core e LINQ

## Objetivos
- Entender persistência de dados e modelagem relacional (PK/FK, relações).
- Conhecer EF Core (Data Annotations e Fluent API).
- Praticar consultas com LINQ e organizar migrações.

## Banco de dados (visão geral)
- Ambiente organizado para guardar e ler informações.
- Conceitos: tabelas, colunas e linhas.
- Analogia: o Excel é uma boa referência visual.

### PK e FK
- Chave primária (PK): identificador único.
- Chave estrangeira (FK): liga tabelas.

### Relacionamentos 1:1, 1:N, N:N
- 1:1 — uma linha se relaciona com exatamente uma em outra tabela.
- 1:N — uma linha se relaciona com várias de outra tabela.
- N:N — muitas linhas de uma tabela se relacionam com muitas de outra.

## ORM e EF Core
- ORM (Object Relational Mapper): “tradutor” entre classes/objetos e tabelas/linhas.
- EF Core: ORM oficial do .NET para focar na regra de negócio.

### Data Annotations
```csharp
public class Cliente
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";
}
```

### Fluent API
```csharp
protected override void OnModelCreating(ModelBuilder mb)
{
    mb.Entity<Cliente>()
      .HasKey(c => c.Id);

    mb.Entity<Cliente>()
      .Property(c => c.Name)
      .IsRequired()
      .HasMaxLength(100);
}
```

## LINQ (consultas)
```csharp
var ultimasTransacoes = await context.Transacoes
    .Where(t => t.CartaoId == cartaoId)
    .OrderByDescending(t => t.When)
    .Take(5)
    .ToListAsync();
```

## Migrações (migrations)
- Versões do esquema do banco; permitem evoluir e reverter alterações com segurança.

## Transações (ACID)
- Pacote de ações que precisam acontecer juntas; se uma falhar, tudo é desfeito.

## Exercício
Modele o esquema para transação de cartão de crédito e extrato mensal. Inclua ao menos:
- Cliente
- Cartão
- Transação
- Fatura/Extrato mensal

---
Navegação: [← Aula 3](Aula-3.md) | [Aula 5 →](Aula-5.md)
