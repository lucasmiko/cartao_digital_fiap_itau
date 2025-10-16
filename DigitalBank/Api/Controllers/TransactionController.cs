using DigitalBank.Api.Application.DTOs;
using DigitalBank.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly TransactionService _transactionService;

    public TransactionsController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost("debit-purchase")]
    public ActionResult<object> ProcessDebitPurchase([FromBody] PurchaseDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = _transactionService.ProcessDebitPurchase(dto.AccountId, dto.CardId, dto.Amount);

        if (result.NotFound)
            return NotFound(new { message = result.Message });

        if (result.Transaction is null)
            return BadRequest(new { message = "Ocorreu um erro na requisição." });

        var payload = new
        {
            result.Transaction.Id,
            result.Transaction.Status,
            result.Message
        };

        if (result.Success)
            return Ok(payload);

        return UnprocessableEntity(payload);
    }

    [HttpPost("credit-purchase")]
    public ActionResult<object> ProcessCreditPurchase([FromBody] PurchaseDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = _transactionService.ProcessCreditPurchase(dto.AccountId, dto.CardId, dto.Amount);

        if (result.NotFound)
            return NotFound(new { message = result.Message });

        if (result.Transaction is null)
            return BadRequest(new { message = "Ocorreu um erro na requisição." });

        var payload = new
        {
            result.Transaction.Id,
            result.Transaction.Status,
            result.Message
        };

        if (result.Success)
            return Ok(payload);

        return UnprocessableEntity(payload);
    }
}