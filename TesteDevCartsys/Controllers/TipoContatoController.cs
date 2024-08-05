using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TesteDevCartsys.Data;
using TesteDevCartsys.Data.Dtos;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Controllers;

[ApiController]
[Route("[controller]")]
public class TipoContatoController : ControllerBase
{
    private TesteDevCartsysContext _context { get; set; }
    private IMapper _mapper { get; set; }

    public TipoContatoController(TesteDevCartsysContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaTipoContato([FromBody] CreateTipoContatoDto tipoContatoDto)
    {
        TipoContato tipoContato = _mapper.Map<TipoContato>(tipoContatoDto);
        var respostaContato = RetornaTipoContatoPorDescricao(tipoContato.Descricao);
        if (respostaContato is OkObjectResult) return Conflict("Já existe um tipo de contato com a mesma descrição");

        _context.TiposContato.Add(tipoContato);
        _context.SaveChanges();
        return CreatedAtAction(
                nameof(RetornaTipoContatoPorId),
                new { id = tipoContato.Id },
                tipoContato
            );
    }

    [HttpGet("{id}")]
    public IActionResult RetornaTipoContatoPorId(int id)
    {
        var tipoContato = _context.TiposContato.FirstOrDefault(tc => tc.Id == id);
        if (tipoContato == null) return NotFound();
        var tipoContatoDto = _mapper.Map<ReadTipoContatoDto>(tipoContato);
        return (Ok(tipoContatoDto));
    }

    [HttpGet("{descricao}")]
    public IActionResult RetornaTipoContatoPorDescricao(string descricao)
    {
        var tipoContato = _context.TiposContato.FirstOrDefault(tc => tc.Descricao.Equals(descricao));
        if (tipoContato == null) return NotFound();
        var tipoContatoDto = _mapper.Map<ReadTipoContatoDto>(tipoContato);
        return (Ok(tipoContatoDto));
    }

    [HttpGet]
    public IEnumerable<ReadTipoContatoDto> RetornaTiposContato([FromQuery] int skip = 0, int take = 10)
    {
        return _mapper.Map<List<ReadTipoContatoDto>>(_context.TiposContato.Skip(skip).Take(take));
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaTipoContato(int id, [FromBody] UpdateTipoContatoDto tipoContatoDto) 
    {
        var tipoContato = _context.TiposContato.FirstOrDefault(tc => tc.Id == id);
        if (tipoContato == null) return NotFound();
        _mapper.Map(tipoContatoDto, tipoContato);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaTipoContato(int id)
    {
        var tipoContato = _context.TiposContato.FirstOrDefault(tc => tc.Id == id);
        if (tipoContato == null) return NotFound();

        _context.Remove(tipoContato);
        _context.SaveChanges();

        return NoContent();
    }
}
