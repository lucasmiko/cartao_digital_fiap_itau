using Microsoft.AspNetCore.Mvc;

namespace cartao_digital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesFileRepo _repo = new();

        // GET api/clientes
        [HttpGet]
        public ActionResult<List<Customer>> GetAll()
        {
            var list = _repo.Load();
            return Ok(list);
        }

        // GET api/clientes/5
        [HttpGet("{id:int}")]
        public ActionResult<Customer> GetById(int id)
        {
            var c = _repo.Load().FirstOrDefault(x => x.Id == id);
            return c is null ? NotFound() : Ok(c);
        }

        // POST api/clientes
        // Body: { "name": "Novo Cliente" } (Id opcional)
        [HttpPost]
        public ActionResult<Customer> Create([FromBody] Customer input)
        {
            var list = _repo.Load();

            // Gera Id simples se não vier no payload
            if (input.Id == 0)
                input.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;

            if (list.Any(x => x.Id == input.Id))
                return Conflict($"Já existe cliente com Id {input.Id}.");

            _repo.Add(input);
            return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
        }

        // PUT api/clientes/5
        // Body: { "name": "Nome Atualizado" }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Customer input)
        {
            var list = _repo.Load();
            var idx = list.FindIndex(x => x.Id == id);
            if (idx < 0) return NotFound();

            list[idx].Name = input.Name;
            _repo.Save(list);
            return NoContent();
        }

        // DELETE api/clientes/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var list = _repo.Load();
            var removed = list.RemoveAll(x => x.Id == id);
            if (removed == 0) return NotFound();

            _repo.Save(list);
            return NoContent();
        }

        // POST api/clientes/seed
        // (Cria alguns registros de exemplo)
        [HttpPost("seed")]
        public IActionResult Seed()
        {
            var seed = new List<Customer>
            {
                new() { Id = 1, Name = "João" },
                new() { Id = 2, Name = "Maria" },
                new() { Id = 3, Name = "Acme Ltda." }
            };
            _repo.Save(seed);
            return Ok(seed);
        }

        // DELETE api/clientes/reset
        // (Apaga o arquivo clientes.json)
        [HttpDelete("reset")]
        public IActionResult Reset()
        {
            try { System.IO.File.Delete("clientes.json"); } catch { /* Nao precisa mandar erro */ }
            return NoContent();
        }
    }
}