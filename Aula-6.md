# Trabalhando com Arquivos no C#

- Encoding

### O que é “Encoding” (em palavras simples)

- Computador **não entende letras**, só números.
- **Encoding** é a “tabela” que **mapeia** cada **caractere** para um **número**.
- Existem várias “tabelas” (encodings). As mais comuns:
    - **ASCII**: bem antigo, 128 símbolos (inglês). **Não tem acentos/emoji** → vira `?`.
    - **Unicode**: “dicionário global” de caracteres (praticamente todos os idiomas).
    - **UTF-8**: **jeito moderno de guardar Unicode**. Leve e padrão na web.
        
        → **Use UTF-8 para quase tudo** no dia a dia.

ABC > 000000101010101001

### Métodos que você vai usar 99% das vezes

- **`File.WriteAllText(path, text, encoding?)`** → cria/substitui um arquivo com texto.
- **`File.ReadAllText(path)`** → lê tudo como uma única string.
- **`File.AppendAllText(path, text)`** → **acrescenta** texto ao final do arquivo.
- **`Directory.CreateDirectory(path)`** → **garante** que a pasta existe.


### Arquitetura desafio MiniCheckout

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
