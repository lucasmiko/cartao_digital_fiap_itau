using System.ComponentModel.DataAnnotations;

namespace DigitalBank.Api.Application.DTOs;

public record DepositDto(
    [Required(ErrorMessage = "O valor para depósito é obrigatório.")]
    decimal Amount
);