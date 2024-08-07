using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TesteDevCartsys.Data;
using TesteDevCartsys.Data.Dtos;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase
{

    private TesteDevCartsysContext _context;
    private IMapper _mapper;

    public PessoaController(TesteDevCartsysContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaPessoa([FromBody] CreatePessoaDto pessoaDto)
    {
        if (VerificaCpfExistente(pessoaDto.Cpf)) return Conflict("CPF já existente na base");

        var pessoa = _mapper.Map<Pessoa>(pessoaDto);
        _context.Pessoas.Add(pessoa);
        _context.SaveChanges();
        return CreatedAtAction(
                nameof(RetornaPessoaPorId),
                new { id = pessoa.Id },
                pessoa
            );
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaPessoa(int id, [FromBody] UpdatePessoaDto pessoaDto)
    {
        var pessoa = _context.Pessoas.FirstOrDefault(c => c.Id == id);
        if (pessoa == null) return NotFound();
        _mapper.Map(pessoaDto, pessoa);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaPessoaParcial(int id, JsonPatchDocument<UpdatePessoaDto> patch)
    {
        var pessoa = _context.Pessoas.FirstOrDefault(c => c.Id == id);
        if (pessoa == null) return NotFound();

        var pessoaAtualizacao = _mapper.Map<UpdatePessoaDto>(pessoa);
        patch.ApplyTo(pessoaAtualizacao, ModelState);

        if (!TryValidateModel(pessoaAtualizacao)) return ValidationProblem(ModelState);
        _mapper.Map(pessoaAtualizacao, pessoa);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet]
    public IEnumerable<ReadPessoaDto> RetornaPessoas(int skip = 0, int take = 10)
    {
        return _mapper.Map<List<ReadPessoaDto>>(_context.Pessoas.ToList().Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RetornaPessoaPorId(int id)
    {
        var pessoa = _context.Pessoas.FirstOrDefault(c => c.Id == id);
        if (pessoa == null) return NotFound();
        var pessoaDto = _mapper.Map<ReadPessoaDto>(pessoa);
        return Ok(pessoaDto);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaPessoa(int id)
    {
        var pessoa = _context.Pessoas.FirstOrDefault(c => c.Id == id);
        if (pessoa == null) return NotFound();
        _context.Remove(pessoa);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet("{cpf}")]
    public bool VerificaCpfExistente(string cpf)
    {
        return _context.Pessoas.Any(p => p.Cpf.Equals(cpf));
    }    
}
