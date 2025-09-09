# Encontro 1: API & Rotas

> Objetivo do dia
> 
> 
> Sair entendendo o que é uma API REST, como funcionam **rotas/controllers** no ASP.NET Core, praticar **tipos e funções** em C# e **testar** tudo via Swagger e Insomnia — com direito a **debug no VS Code**.
>

## O que é uma API?

Uma API é como o garçom entre você (aplicativo cliente) e a cozinha (servidor/banco).

## REST

- (Representational State Transfer)

- **Métodos HTTP**:
    - `GET` — Buscar dados (não altera o servidor, **idempotente**)
    - `POST` — Criar dados (pode alterar o estado, **não idempotente**)
    - `PUT` — Atualizar **tudo** de um recurso (idempotente)
    - `PATCH` — Atualizar **parte** do recurso
    - `DELETE` — Remover recurso (idempotente)


## Stateless (sem estado) 

Sem estado” significa: o servidor não guarda memória da conversa entre requisições.

## HTTP

- **Status codes**:
    - `200 OK` — deu certo
    - `201 Created` — criado com sucesso
    - `400 Bad Request` — erro do cliente (param faltando, formato errado)
    - `404 Not Found` — recurso não existe
    - `500 Internal Server Error` — exceção no servidor

## O que é um Controller?

- Uma **classe** C# que **agrupa endpoints** (rotas) relacionados.

## Tipos

- Definição: Um tipo define a natureza de um dado - o que ele pode armazenar e como pode ser manipulado.

> Os tipos importam muito no C#!

### Tipos por valor (value types)

- Copiam valores, variaveis nao compartilham memoria.

### Tipos por referência (reference types)

- Copiam a referencia, variaveis compartilham o mesmo objeto.

## Funções (Métodos)

int Somar(int a, int b)
{
    return a + b;
}