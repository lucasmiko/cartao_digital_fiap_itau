using DigitalBank.Api.Application.DTOs;
using DigitalBank.Api.Application.Services;
using DigitalBank.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly CardService _cardService;
    private readonly ReportingService _reportingService;

    public CardsController(CardService cardService, ReportingService reportingService)
    {
        _cardService = cardService;
        _reportingService = reportingService;
    }

    [HttpPost("debit")]
    public ActionResult<Card> CreateDebitCard([FromQuery] int accountId)
    {
        if (accountId <= 0)
            return BadRequest("O Id da conta é obrigatório.");

        var card = _cardService.CreateDebitCard(accountId);
        if (card == null)
            return NotFound("Ocorreu um erro ao criar o cartão. Tente novamente.");

        return CreatedAtAction(nameof(GetCardsByAccount), new { accountId = accountId }, card);
    }

    [HttpPost("credit")]
    public ActionResult<Card> CreateCreditCard([FromQuery] int accounId, CreateCreditCardDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        if (accounId <= 0)
            return BadRequest("O Id da conta é obrigatório.");

        var card = _cardService.CreateCreditCard(accounId, dto.CreditLimit);
        if (card == null)
            return NotFound("Ocorreu um erro ao criar o cartão. Tente novamente.");

        return CreatedAtAction(nameof(GetCardsByAccount), new { accountId = accounId }, card);
    }

    [HttpGet]
    public ActionResult<List<Card>> GetCardsByAccount([FromQuery] int accountId)
    {
        if (accountId <= 0)
            return BadRequest("O Id da conta é obrigatório.");

        var cards = _cardService.GetCardsByAccount(accountId);
        if (cards == null || !cards.Any())
            return NotFound("Nenhum cartão encontrado.");

        return Ok(cards);
    }

    [HttpPost("{id:int}/invoice/save")]
    public ActionResult<object> GenerateAndSaveCardInvoice([FromRoute] int id, [FromQuery] int month, [FromQuery] int year)
    {
        if (month < 1 || month > 12 || year < 1)
            return BadRequest("Mês ou ano inválido.");

        var fileName = _reportingService.GenerateAndSaveCardInvoiceReport(id, month, year);
        if (fileName == null)
            return NotFound("Ocorreu um erro. Tente novamente.");

        return Ok(new { message = "Fatura gerada com sucesso.", fileName });
    }
}