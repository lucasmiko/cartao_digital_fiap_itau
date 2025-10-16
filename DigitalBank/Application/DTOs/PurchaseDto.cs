using System.ComponentModel.DataAnnotations;

namespace DigitalBank.Api.Application.DTOs;

public record PurchaseDto(
    [Required(ErrorMessage = "O Id da conta é obrigatório.")]
    int AccountId,

    [Required(ErrorMessage = "O Id do cartão é obrigatório.")]
    int CardId,

    [Range(0.01, double.MaxValue, ErrorMessage = "O valor da compra deve ser maior que zero.")]
    decimal Amount
);