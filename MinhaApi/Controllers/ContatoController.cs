using Microsoft.AspNetCore.Mvc;
using MinhaApi.Contexto;
using MinhaApi.Entidade;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContexto contexto; //Injeção de independencia
        public ContatoController(AgendaContexto agendaContexto)
        {
            contexto = agendaContexto;
        }

        [HttpPost("CriarContato")]
        public IActionResult Create(Contato contato)
        { //Implementando o Crud
            contexto.Add(contato);
            contexto.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new {id = contato.Id}, contato);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contatoBanco = contexto.contatos.Find(id);
            if (contatoBanco == null)
            {
                return NotFound();
            }

            return Ok(contatoBanco); 
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatoBanco = contexto.contatos.Where(a => a.Nome.Contains(nome));
            if (contatoBanco == null)
            {
                return NotFound();
            }

            return Ok(contatoBanco);
        }



        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var contatoBanco = contexto.contatos.Find(id);
            if (contatoBanco == null)
            {
                return NotFound();  
            }
            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            contexto.contatos.Update(contatoBanco);
            contexto.SaveChanges();

            return Ok(contatoBanco);

        }

        [HttpDelete("{id}")]
        public ActionResult Deletar(int id)
        {
            var contatoBanco = contexto.contatos.Find(id);
            if (contatoBanco == null)
            {
                return NotFound();
            }

            contexto.contatos.Remove(contatoBanco);
            contexto.SaveChanges();

            return NoContent();

        }
    }
}
