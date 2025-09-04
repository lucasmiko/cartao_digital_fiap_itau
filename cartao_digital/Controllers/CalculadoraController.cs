using Microsoft.AspNetCore.Mvc;

namespace cartao_digital.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculadoraController : ControllerBase
{
    [HttpGet("somar")]
    public IActionResult Somar([FromQuery] double a, [FromQuery] double b, [FromQuery] double c)
    {
        var resultado = MathUtils.Somar(a, b, c);
        return Ok(new { Resultado = resultado });
    }
}