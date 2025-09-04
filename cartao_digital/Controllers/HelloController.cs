using Microsoft.AspNetCore.Mvc;

namespace cartao_digital.Controllers;

// Decorator > Tag
[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    // GET /api/hello
    [HttpGet]
    public ActionResult<object> GetHello()
    {
        return Ok(new { message = "API funcionando! 👋" });
    }

    // api/hello/greet/{name} 
    [HttpGet("greet/{name}")]
    public ActionResult<object> GetHelloGreet(string name)
    {
        return Ok(new { message = $"Olá, {name}! Bem-vindo à API! 👋" });
    }

    [HttpGet("echo")]
    public ActionResult<object> GetHelloEcho([FromQuery] string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return BadRequest(new { error = "O parâmetro 'message' é obrigatório." });
        }

        return Ok(new { echoedMessage = message });
    }
}