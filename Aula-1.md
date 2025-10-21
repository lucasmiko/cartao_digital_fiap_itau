# Aula 1 — API REST, Rotas e Tipos

## Objetivos
- Entender o que é uma API REST e como o HTTP funciona.
- Criar e testar controllers/endpoints no ASP.NET Core.
- Revisar tipos por valor/referência e funções em C#.
- Testar via Swagger e Insomnia e debugar no VS Code.

## API e REST

- API é o “garçom” entre cliente (app) e servidor (dados).
- REST (Representational State Transfer) usa verbos HTTP e recursos.
- Métodos HTTP mais usados:
  - `GET` — Buscar dados (idempotente; não altera estado).
  - `POST` — Criar dados (não idempotente).
  - `PUT` — Atualizar o recurso inteiro (idempotente).
  - `PATCH` — Atualizar parte do recurso.
  - `DELETE` — Remover recurso (idempotente).

### Stateless (sem estado)
- O servidor não mantém contexto entre requisições; cada chamada é completa em si.

### HTTP — Status Codes comuns
- `200 OK` — sucesso
- `201 Created` — criado com sucesso
- `400 Bad Request` — erro do cliente (parâmetro/formato inválido)
- `404 Not Found` — recurso não existe
- `500 Internal Server Error` — erro no servidor

## Controllers (ASP.NET Core)
- Classe C# que agrupa endpoints (rotas) relacionados.
- Ex.: `CustomersController` concentra rotas de clientes.

## Tipos em C#

> Tipagem é fundamental para correção e expressividade no C#.

### Tipos por valor (value types)
- Copiam o valor; variáveis não compartilham a mesma memória.

### Tipos por referência (reference types)
- Copiam a referência; variáveis apontam para o mesmo objeto.

## Funções (métodos)

```csharp
int Somar(int a, int b)
{
    return a + b;
}
```

## Prática
- Subir uma Web API mínima e criar `HelloController` com `GET /hello`.
- Testar via Swagger e Insomnia.
- Adicionar um `POST /echo` que retorna o corpo recebido.
- Colocar breakpoint e debugar no VS Code.

## Recursos
- Documentação de Controllers: https://learn.microsoft.com/aspnet/core/mvc/controllers/actions

---
Navegação: [Aula 2 →](Aula-2.md)
