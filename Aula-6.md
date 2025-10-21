# Aula 6 — Arquivos e Encoding no C#

## Objetivos
- Ler e escrever arquivos de texto com segurança (UTF-8).
- Usar `Directory.CreateDirectory` para garantir existência de pastas.
- Estruturar um repositório simples baseado em arquivos.

## Encoding (explicação rápida)
- O computador entende números; encoding mapeia caracteres para números.
- Encodings comuns:
  - ASCII: 128 símbolos, sem acentos/emoji.
  - Unicode: dicionário global de caracteres.
  - UTF-8: forma moderna de armazenar Unicode (leve e padrão web).
- Regra prática: prefira UTF-8.

## APIs úteis (99% dos casos)
- `File.WriteAllText(path, text, encoding?)`: cria/substitui arquivo com texto.
- `File.ReadAllText(path)`: lê todo o conteúdo como string.
- `File.AppendAllText(path, text)`: acrescenta texto ao final do arquivo.
- `Directory.CreateDirectory(path)`: garante que a pasta exista.

## Desafio: MiniCheckout (arquitetura sugerida)
```
/MiniCheckout
  /Api
    Program.cs
    /DTOs
      CheckoutDtos.cs
  /Application
    /Interfaces
      IProductRepository.cs
    /Discounts
      IDiscountStrategy.cs
      NoDiscount.cs
      TenPercentDiscount.cs
    /Services
      CheckoutService.cs
  /Domain
    Product.cs
  /Infrastructure
    /Repositories
      InMemoryProductRepository.cs
```

---
Navegação: [← Aula 5](Aula-5.md) | [Aula 7 →](Aula-7.md)
