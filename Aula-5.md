# Aula 5 — Arquitetura em Camadas, SOLID e DI

## Objetivos
- Separar responsabilidades por camadas para legibilidade e testabilidade.
- Aplicar princípios SOLID e injeção de dependências.
- Conhecer padrões: Repository, Service, DTO e Mapper.

## Arquitetura em camadas
- Domain (Core): entidades e regras de negócio.
- Application/Services: orquestra regras, coordena repositórios.
- Infrastructure/Repositories: acesso a dados (arquivos/BD).
- API (Controllers): expõe endpoints HTTP.

Fluxo comum:

```csharp
[ API (Controllers) ]
         |
         v
[ Application / Services ]       // Regras de negócio orquestradas
         |
         v
[ Infrastructure / Repositories ] // Lê/Grava
         |
         v
[ Data (arquivos, bancos SQL/NoSQL) ]

// Transversal ao sistema
[ Domain ] // Entidades/valores e regras puras
```

## SOLID

### S — SRP (Single Responsibility Principle)
- Uma classe deve ter um único motivo para mudar.

### O — OCP (Open/Closed Principle)
- Aberto para extensão, fechado para modificação.

### L — LSP (Liskov Substitution Principle)
- Se B é subtipo de A, B deve poder ser usado onde se espera A, sem surpresas.

```csharp
public interface INotificador
{
    void Enviar(string mensagem);
}

public class EmailNotificador : INotificador
{
    public void Enviar(string mensagem)
    {
        // Regras para enviar e-mail (SMTP, validação etc.)
    }
}

public class SmsNotificador : INotificador
{
    public void Enviar(string mensagem)
    {
        // Regras específicas para SMS (limite, fila etc.)
    }
}

void AvisarNossoCliente(INotificador notificador)
{
    notificador.Enviar("Mensagem qualquer!");
}

AvisarNossoCliente(new EmailNotificador());
AvisarNossoCliente(new SmsNotificador());
```

### I — ISP (Interface Segregation Principle)
- Interfaces pequenas e focadas; não forçar contratos desnecessários.

### D — DIP (Dependency Inversion Principle)
- Depender de abstrações (interfaces), não de classes concretas.

## Injeção de Dependência (DI)
- Entregar às classes suas dependências já prontas, via container.

```csharp
builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
```

### Ciclos de vida
- Transient: nova instância a cada resolução (copo descartável).
- Scoped: uma por requisição HTTP (caneca do funcionário).
- Singleton: uma para a aplicação inteira (garrafa térmica).

## Padrões

### Repository
- Porta de acesso aos dados; isola infraestrutura.

### Service
- Orquestra regras de negócio e coordena repositórios; facilita testes.

### DTO
- Data Transfer Object: contrato de entrada/saída de dados.

```csharp
public class CafeDto
{
    public string Tipo { get; set; } = "";
    public bool SemCafeina { get; set; }
}
```

### Mapper
- Tradutor entre DTO e entidade do domínio.

Analogia:
- Repository: bibliotecário
- Service: professor
- DTO: envelope com um resumo
- Mapper: garante o idioma certo entre camadas

## Prática
- Criar interfaces e implementações de repositórios/serviços.
- Registrar as dependências no `Program.cs` e consumir nos controllers.

---
Navegação: [← Aula 4](Aula-4.md) | [Aula 6 →](Aula-6.md)
