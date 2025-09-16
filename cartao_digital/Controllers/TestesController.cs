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

        [HttpGet("transferir")]
        public ActionResult<object> TesteTransferencia([FromQuery] decimal valor)
        {
            var pf = new ClientePF(1, "João Silva", "123.456.789-00");
            var pj = new ClientePJ(2, "Loja XYZ", "12.345.678/0001-99");

            pf.Depositar(300m);
            pj.Depositar(50m);

            Console.WriteLine($"Antes da transferência: PF saldo={pf.Saldo}, PJ saldo={pj.Saldo}");

            try
            {
                pf.Transferir(pj, valor);
                Console.WriteLine($"Transferência de {valor:C} realizada com sucesso.");
                Console.WriteLine($"Depois da transferência de {valor:C}: PF saldo={pf.Saldo}, PJ saldo={pj.Saldo}");

                return Ok(new { mensagem = "Transferência realizada com sucesso.", saldoPF = pf.Saldo, saldoPJ = pj.Saldo });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Erro na transferência: {ex.Message}");
                return Ok(new { mensagem = $"Erro na transferência: {ex.Message}", saldoPF = pf.Saldo, saldoPJ = pj.Saldo });
            }
        }

        [HttpGet("vip")]
        public ActionResult<object> TesteVip([FromQuery] decimal limite)
        {
            var vip = new ClientePersonalite(3, "Maria VIP", limite);
            vip.Depositar(1000m);

            var passos = new List<object>();
            var saques = new decimal[] { 100m, 150m, 110m };

            Console.WriteLine($"Cliente VIP iniciou com saldo {vip.Saldo} e limite de saque diário {vip.LimiteSaqueDiario}");

            foreach (var saque in saques)
            {
                try
                {
                    var antesDoSaque = vip.Saldo;
                    vip.Sacar(saque);
                    var depoisDoSaque = vip.Saldo;

                    Console.WriteLine($"Sacou {saque:C}, saldo antes: {antesDoSaque:C}, saldo depois: {depoisDoSaque:C}");
                    passos.Add(new { saque, ok = true, saldoAntes = antesDoSaque, saldoDepois = depoisDoSaque });

                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Erro ao sacar {saque:C}: {ex.Message}");
                    passos.Add(new { saque, ok = false, erro = ex.Message, saldoAtual = vip.Saldo });
                }
            }

            Console.WriteLine($"Cliente VIP finalizou com saldo {vip.Saldo}");
            return Ok(new { messagem = "Teste de Cliente VIP concluído.", limiteDiario = limite, saldoFinal = vip.Saldo, passos });
        }
    }
}