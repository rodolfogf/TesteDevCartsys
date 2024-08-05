using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TesteDevCartsys.Data;
using TesteDevCartsys.Data.Dtos;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Controllers;

[ApiController]
[Route("[controller]")]
public class TipoPessoaController : ControllerBase
{
    private TesteDevCartsysContext _context { get; set; }
    private IMapper _mapper { get; set; }

    public TipoPessoaController(TesteDevCartsysContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaTipoPessoa([FromBody] CreateTipoPessoaDto tipoPessoaDto)
    {
        TipoPessoa tipoPessoa = _mapper.Map<TipoPessoa>(tipoPessoaDto);
        var respostaTipoPessoa = RetornaTipoPessoaPorDescricao(tipoPessoa.Descricao);
        if (respostaTipoPessoa is OkObjectResult) return Conflict("Já existe um tipo de pessoa com a mesma descrição");

        _context.TiposPessoa.Add(tipoPessoa);
        _context.SaveChanges();
        return CreatedAtAction(
                nameof(RetornaTipoPessoaPorId),
                new { id = tipoPessoa.Id },
                tipoPessoa
            );
    }

    [HttpGet("{id}")]
    public IActionResult RetornaTipoPessoaPorId(int id)
    {
        var tipoPessoa = _context.TiposPessoa.FirstOrDefault(tc => tc.Id == id);
        if (tipoPessoa == null) return NotFound();
        var tipoPessoaDto = _mapper.Map<ReadTipoPessoaDto>(tipoPessoa);
        return (Ok(tipoPessoaDto));
    }

    [HttpGet("{descricao}")]
    public IActionResult RetornaTipoPessoaPorDescricao(string descricao)
    {
        var tipoPessoa = _context.TiposPessoa.FirstOrDefault(tp => tp.Descricao.Equals(descricao));
        if (tipoPessoa == null) return NotFound();
        var tipoPessoaDto = _mapper.Map<ReadTipoPessoaDto>(tipoPessoa);
        return (Ok(tipoPessoaDto));
    }

    [HttpGet]
    public IEnumerable<ReadTipoPessoaDto> RetornaTiposPessoa([FromQuery] int skip = 0, int take = 10)
    {
        return _mapper.Map<List<ReadTipoPessoaDto>>(_context.TiposPessoa.Skip(skip).Take(take));
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaTipoPessoa(int id, [FromBody] UpdateTipoPessoaDto tipoPessoaDto) 
    {
        var tipoPessoa = _context.TiposPessoa.FirstOrDefault(tp => tp.Id == id);
        if (tipoPessoa == null) return NotFound();
        _mapper.Map(tipoPessoaDto, tipoPessoa);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaTipoPessoa(int id)
    {
        var tipoPessoa = _context.TiposPessoa.FirstOrDefault(tp => tp.Id == id);
        if (tipoPessoa == null) return NotFound();

        _context.Remove(tipoPessoa);
        _context.SaveChanges();

        return NoContent();
    }
}
