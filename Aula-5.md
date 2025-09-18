# Arquitetura, SOLID e Injeção de Dependência

## Arquitetura em camadas

Definicao: separar o nosso projeto por responsabilidades.

### Beneficios

- Codigo mais legivel
- O nosso codigo fica mais testavel
- Torna mais simples novas implementacoes sem quebrar outras partes

Domain (core): regras de negocios.
  - Cardapio e receitas 

Services: comunicacao com os repositorios. Executa as regras de negocio
  - Cozinha

Infraestrutura/Repositories: Ponte entre a aplicacao e os dados
 - Despensa/Fornecedor

API (controller / endpoits): comunicacao entre o client e o nosso core
  - Garcom

Fluxo comum:

Client (frontend) > API (Controller) > Services (regras) > Repository (acesso dados) > Resposta (DTO)

```csharp
[ API (Controllers) ]
          |
          v
[ Application / Services ]  <-- Regras de negócio orquestradas
          |
          v
[ Infrastructure / Repositories ]  <-- Lê/Grava
          |
          v
[ Data (arquivos, Banco de dados (SQL / NoSql)) ]


// Algo que pode ser comum em todas as camadas, e que dita o nosso sistema
[ Domain ]  <-- Entidades/valores e regras puras (transversal)

```

## SOLID

### S - SRP - Single Responsibility Principle

Definicao: uma classe deve ter um objetivo, um motivo para mudar.

### O - OCP - Open/Closed Principle

Definicao: aberto para extensao, fechado para modificacao. Para adicionar um comportamento, criamos algo novo, nao reescrevemos o que ja esta funcionando. 

### L - LSP - Liskov Substitution Principle

Definicao: se um tipo  B é o mesmo que do tipo A, devmos usar o B onde se espera o A, sem complicacoes.

```csharp

public interface INotificador
{
    void Enviar(string mensagem);
}

public class EmailNotificador : INotificador
{
    public void Enviar(string mensagem){
        // Regra para enviar um email
        // SMTP > Servico de email contratado (gmail. outlook)
        // validar qual email vai receber
        //Se o email veio com regex bem construido, com @dominio.com / .com.br
        // retornar junto uma mensagem de olhe sua caixa de spam
    }
}

public class SMSNotificador : INotificador
{
    public void Enviar(string mensagem){
        // Tem regras especificas do nosso servico de mensagem de texto
        // Qual o limite de tentativas?
        // retornar junto uma mensagem de espera
    }

    private string RetornarOtempoDeEspera(){}
}

void AvisarNossoCliente(INotificador notificador){
    notificador.Enviar("Mensagem qualquer!");
}

AvisarNossoCliente(new EmailNotificador());
AvisarNossoCliente(new SMSNotificador());

```

### I - ISP - Interface Segregation Principle

Definicao: interfaces que sao pequenas e focadas em um objetivo. Nao deve obrigar alguma parte do nosso projeto a implementar algo que ele nao usa.

Email > Validar o dominio @...

O que é uma interface? Um contrato.

### D - Dependency Inversion Principle

Definicao: depender de abstracoes (interfaces) e nao de classes concretas.

Interface de controller

```csharp
public interface IController 
{
    GetAll();
    GetById();
    Create();
    Update();
    Delete();
}

public class PurchaseController : IController
{

}

--Repository
    --Interfaces
        --Repositories
--Service
    --Interfaces
        --Services
--Controller
    --Interfaces
        --Controllers
```

## Injeção de Dependência (DI)

Definicao: É o meio pelo qual entregamos a uma classe tudo o que ela precisa para funcionar, ja pronto para uso.

```csharp
// builder.Services.AddSingleton<ITransactionRepository, FileTransactionRepository>

builder.Services.AddSingleton<ITransactionRepository, FileDevTransactionRepository>
```

- Tem ciclos de vida
    - Transient: objeto novo novo a cada vez
        - copo descartavel
    - Scoped: um por requisicao HTTP
        - caneca de um funcionario
    - Singleton: uma para a aplicacao inteira
        - maquina de cafe, garrafa termica

Obter o cafe!

# Padrões

## Repository

Definicao: uma porta de acesso aos nossos dados.

Beneficio: isola infraestrutura, se o formato do arquivo ou dado que estamos trabalhando mudar a regra nao sofre.

## Service

Definicao: sao como um orquestrador da regra de negocios. Valida e cordena os repositorios.

Beneficio: concentra a logica dentro de um so lugar. Controller ele ficam com uma qualidade mais fina. Facil de testar.

## DTO

Definicao: Data Transfer Object. Contrato de entrada e saida de dados. Um objeto de trnaferencia.

```csharp
// Classe
public class Cafe
{
    Quantidade

    Temperatura

    Tipo

    ComSemCafeina
}

public class CafeDTO
{
    Tipo

    ComSemCafeina, required, int/string...

    // Gravar no banco as informacoes consulta por Id
}
```

## Mapper

Definicao: tradutor entre o DTO - Entidade do dominio

Analogia

    - Repository: bibliotecario
    - Services: professor
    - DTO: envelope com o um resumo
    - Mapper: garante que o resumo ele esteja idioma correto


