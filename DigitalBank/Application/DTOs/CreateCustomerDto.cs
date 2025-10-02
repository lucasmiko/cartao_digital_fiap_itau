using System.ComponentModel.DataAnnotations;

namespace DigitalBank.Api.Application.DTOs;

public record CreateCustomerDto(
    [Required(ErrorMessage = "O nome é obrigatório")]
    string Name,
    [Required(ErrorMessage = "O CPF é obrigatório")]
    string Cpf
);
