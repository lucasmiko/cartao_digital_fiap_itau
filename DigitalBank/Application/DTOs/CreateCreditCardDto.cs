using System.ComponentModel.DataAnnotations;

namespace DigitalBank.Api.Application.DTOs;

public record CreateCreditCardDto(
    [Range(1, double.MaxValue, ErrorMessage = "O limite do cartão deve ser maior que zero.")]
    decimal CreditLimit
);