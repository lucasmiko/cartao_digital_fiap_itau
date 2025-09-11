using Microsoft.AspNetCore.Mvc;

namespace cartao_digital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestesController : ControllerBase
    {
        [HttpGet()]
        public ActionResult<object> TesteUmaAcao([FromQuery] string teste)
        {
            var a = teste.Trim().ToLowerInvariant();

            List<string> acoesPraTeste = a switch
            {
                "if" => ConceitosService.IfDemo(),
                "else" => ConceitosService.ElseDemo(),
                "switch" => ConceitosService.SwitchDemo(),
                "for" => ConceitosService.ForDemo(),
                "foreach" => ConceitosService.ForEachDemo(),
                "dowhile" => ConceitosService.DoWhileDemo(),
                "linq" => ConceitosService.LinqDemo(),
                _ => new List<string> { "Ação não reconhecida. Use: if, else, switch, for, foreach, dowhile, linq." }
            };

            return Ok(new { acao = a, resultados = acoesPraTeste });
        }
    }
}