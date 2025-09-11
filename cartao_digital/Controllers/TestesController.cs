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

        [HttpGet("cliente")]
        public ActionResult<object> TesteCliente()
        {
            var pf = new ClientePF(1, "João Silva", "123.456.789-00");
            var pj = new ClientePJ(2, "Loja XYZ", "12.345.678/0001-99");

            pf.Depositar(100m);
            pj.Depositar(100m);

            pf.Sacar(50m);
            pj.Sacar(50m);

            Console.WriteLine(pf.ToString());
            Console.WriteLine(pj.ToString());

            return Ok();
        }
    }
}