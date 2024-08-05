using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TesteDevCartsys.Controllers.Dtos;
using TesteDevCartsys.Data;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Controllers;

[ApiController]
[Route("[controller]")]
public class ContatoController : ControllerBase
{

    private TesteDevCartsysContext _context;
    private IMapper _mapper;

    public ContatoController(TesteDevCartsysContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaContato([FromBody] CreateContatoDto contatoDto)
    {
        var contato = _mapper.Map<Contato>(contatoDto);
        _context.Contatos.Add(contato);
        _context.SaveChanges();
        return CreatedAtAction(
                nameof(RetornaContatoPorId),
                new { id = contato.Id },
                contato
            );
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaContato(int id, [FromBody] UpdateContatoDto contatoDto)
    {
        var contato = _context.Contatos.FirstOrDefault(c => c.Id == id);
        if (contato == null) return NotFound();
        _mapper.Map(contatoDto, contato);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaContatoParcial(int id, JsonPatchDocument<UpdateContatoDto> patch)
    {
        var contato = _context.Contatos.FirstOrDefault(c => c.Id==id);
        if (contato == null) return NotFound();

        var contatoAtualizacao = _mapper.Map<UpdateContatoDto>(contato);
        patch.ApplyTo(contatoAtualizacao, ModelState);

        if (!TryValidateModel(contatoAtualizacao)) return ValidationProblem(ModelState);
        _mapper.Map(contatoAtualizacao, contato);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet]
    public IEnumerable<ReadContatoDto> RetornaContatos(int skip = 0, int take = 10)
    {
        return _mapper.Map<List<ReadContatoDto>>(_context.Contatos.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RetornaContatoPorId(int id) 
    {
        var contato = _context.Contatos.FirstOrDefault(c => c.Id ==  id);
        if (contato == null) return NotFound();
        var contatoDto = _mapper.Map<ReadContatoDto>(contato);
        return Ok(contatoDto);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaContato(int id)
    {
        var contato = _context.Contatos.FirstOrDefault(c => c.Id == id);
        if (contato == null) return NotFound();
        _context.Remove(contato);
        _context.SaveChanges();

        return NoContent();
    }

}
