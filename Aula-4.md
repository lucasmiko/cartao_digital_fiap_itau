# Persistência de Dados

Persistir os dados é **guardar as informações** para que elas continuem existindo mesmo se o servidor ou dispositivo **desligar temporariamente**.

---

## Banco de dados

Lugar **organizado** para guardar e ler informações.

- **Tabelas**
- **Colunas**
- **Linhas**

*Analogia:* o **Excel**, de forma visual, é uma boa analogia para um banco de dados.

---

### Chaves primárias e chaves estrangeiras

- **Chave primária (PK):** identificador **único**.
- **Chave estrangeira (FK):** usada para **ligar tabelas**.

Exemplo:
```

Cartão 1225645
    Cliente
    Nome
    Saldo
    Data de nascimento
    Endereço

Cartão 365864
    ClienteId

```

## Relacionamentos 1:1, 1:N, N:N

- **1:1** — Uma linha de uma tabela se relaciona com **exatamente uma** linha em outra tabela.
- **1:N** — Uma linha de uma tabela se relaciona com **várias** de outra.
- **N:N** — Muitas linhas de uma tabela se relacionam com **muitas** linhas de outra.

Exemplo:
```

Alunos — Cursos

AlunoCurso
    Id
    AlunoId
    CursoId

````

### ORM

**ORM (Object Relational Mapper):** 

Definição: “tradutor” entre **classes/objetos** e **tabelas/linhas**.


## EF Core

ORM oficial do .NET; permite **concentrar na regra de negócio** enquanto ele cuida dos assuntos complexos do banco de dados.

- **Data Annotations**
  
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

**Fluent API**

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

### LINQ

Definição: “**português do C#**”. Trabalha **listas/coleções** (e entidades via EF) de forma simples.

```csharp
var ultimasTransacoes = await context.Transacoes
    .Where(t => t.CartaoId == cartaoId)
    .OrderByDescending(t => t.When)
    .Take(5)
    .ToListAsync();
```


## Migrations (migrações)

**Migrações** são **versões do esquema** do banco de dados.
Permitem que todos fiquem na **mesma página** e possibilitam **reverter** uma versão que tenha causado bug.


## Transações

**Transação** (no banco) é um **pacote de ações** que **precisam acontecer juntas**.
Se uma falha, **todas são desfeitas** para manter a consistência.


## DESAFIO 3

**Pergunta:** qual seria o **modelo de dados** do banco para ter uma **transação de cartão de crédito** funcionando e gerando um **extrato mensal**?

Dicas mínimas (ponto de partida):

* **Cliente**
* **Cartão**
* **Transação**
* **Fatura/Extrato mensal**

> Cliente… Cartão… (complete o diagrama com PK/FK e campos essenciais).
