# Aula 3 — POO: Herança, Encapsulamento, Polimorfismo e Exceções

## Objetivos
- Aplicar princípios de Programação Orientada a Objetos no domínio do banco.
- Reduzir duplicação e tornar o código mais expressivo e seguro.

## Herança
- Classe base com características comuns; classes filhas reaproveitam e especializam comportamentos.
- Evita duplicação e melhora a organização.

Exemplo de hierarquia:
- `Base` → possui `Id`.
- `Cliente` → herda de `Base`.
- `Conta` → herda de `Base`.
- `Cartao` → herda de `Base`.

## Encapsulamento
- Proteger dados que não devem ser expostos diretamente.
- Benefícios: manter regras de negócio, evitar usos incorretos e vazamentos de estado.

## Polimorfismo
- Métodos com o mesmo nome podem ter comportamentos diferentes conforme o tipo concreto.
- Útil para expor operações em alto nível e variar implementações.

## Exceções
- Sinalizam desvios do fluxo esperado em tempo de execução.
- Lançar exceções claras e do tipo correto (ex.: `ArgumentNullException`).

## Prática
- Modelar `Cliente`, `Conta` e `Cartao` a partir de uma classe base com `Id`.
- Aplicar encapsulamento em estados sensíveis (ex.: saldo) por meio de métodos.
- Usar polimorfismo para operações que variam entre tipos de cartão.
- Lançar exceções apropriadas quando invariantes forem violadas.

---
Navegação: [← Aula 2](Aula-2.md) | [Aula 4 →](Aula-4.md)
