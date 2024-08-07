using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TesteDevCartsys.Data;
using TesteDevCartsys.Models;
using TesteDevCartsys.Views;
using TesteDevCartsys.Services;

namespace TesteDevCartsys.Controllers;

[ApiController]
[Route("[controller]")]
public class RelatorioController : ControllerBase
{
    private TesteDevCartsysContext _context;
    private IMapper _mapper;
    private readonly PdfService _pdfService;
    public RelatorioController(TesteDevCartsysContext context, IMapper mapper, PdfService pdfService)
    {
        _context = context;
        _mapper = mapper;
        _pdfService = pdfService;
    }    

    public async Task<IActionResult> GerarRelatorioPdf(Filtro filtro)
    {
        var linhasRelatorio = new List<LinhaRelatorio>();

        var pessoasRelatorio = new List<Pessoa>();
        var pessoasFiltroTipoContato = new List<Pessoa>();
        foreach (TipoPessoa tp in filtro.tiposPessoa)
        {
            var pessoasTipoPessoa = _context.Pessoas
                .Where(p => p.Contatos
                .Any(c => c.TipoContato.Descricao == tp.Descricao))
                .ToList();

            pessoasFiltroTipoContato.AddRange(pessoasTipoPessoa);
        }

        foreach (TipoContato tc in filtro.tiposContato)
        {
            var pessoasTipoContato = pessoasFiltroTipoContato
                .Where(p => p.Contatos
                .Any(c => c.TipoPessoa.Descricao == tc.Descricao))
                .ToList();

            pessoasRelatorio.AddRange(pessoasTipoContato);
        }

        foreach (var pessoa in pessoasRelatorio)
        {
            foreach (var contato in pessoa.Contatos)
            {
                var linha = new LinhaRelatorio()
                {
                    NomePessoa = pessoa.Nome,
                    DescricaoContato = contato.Descricao,
                    DescricaoTipoPessoa = contato.TipoPessoa.Descricao,
                    DescricaoTipoContato = contato.TipoContato.Descricao
                };
                linhasRelatorio.Add(linha);
            }
        }        

        var pdf = await _pdfService.GeneratePdfAsync(linhasRelatorio);

        return File(pdf, "application/pdf", "Relatorio.pdf");
    }
}

