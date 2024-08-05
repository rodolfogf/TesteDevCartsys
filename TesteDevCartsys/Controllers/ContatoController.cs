using Microsoft.AspNetCore.Mvc;
using TesteDevCartsys.Data;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Controllers;

[ApiController]
[Route("[controller]")]
public class ContatoController : ControllerBase
{

    private static int id = 0;
    private TesteDevCartsysContext _context;

    public ContatoController(TesteDevCartsysContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AdicionaContato([FromBody] Contato contato)
    {
        _context.Contatos.Add(contato);
        _context.SaveChanges();
        return CreatedAtAction(
                nameof(RetornaContatoPorId),
                new {id = contato.Id}, 
                contato
            );
    }

    [HttpGet]
    public IEnumerable<Contato> RetornaContatos(int skip = 0, int take = 10)
    {
        return _context.Contatos.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RetornaContatoPorId(int id) 
    {
        var contato = _context.Contatos.FirstOrDefault(c => c.Id ==  id);
        if (contato == null) return NotFound();
        return Ok(contato);
    }

}
