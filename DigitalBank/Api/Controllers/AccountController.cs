using DigitalBank.Api.Application.DTOs;
using DigitalBank.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountsController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public ActionResult<Account> CreateAccont([FromQuery] int customerId)
    {
        if (customerId <= 0)
            return BadRequest("O Id do cliente é obrigatório.");

        var account = _accountService.CreateForCustomer(customerId);
        if (account == null)
            return NotFound("Cliente não encontrado.");

        return CreatedAtAction(nameof(GetBalance), new { id = account.Id }, account);
    }

    [HttpPost("{id:int}/deposit")]
    public ActionResult<object> Deposit([FromRoute] int id, [FromBody] DepositDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = _accountService.Deposit(id, dto.Amount);
        if (result == null)
            return NotFound("Conta não encontrada ou valor inválido.");

        return Ok(new { accountId = result.Id, newBalance = result.Balance });
    }

    [HttpGet("{id:int}/balance")]
    public ActionResult<decimal> GetBalance([FromRoute] int id)
    {
        var balance = _accountService.GetBalance(id);
        if (balance == null)
            return NotFound("Conta não encontrada.");

        return Ok(new { accounId = id, balance });
    }
}