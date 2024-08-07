using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TesteDevCartsys.Data;
using TesteDevCartsys.Models;
using TesteDevCartsys.Views;
using TesteDevCartsys.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace TesteDevCartsys.Controllers;

[ApiController]
[Route("[controller]")]
public class RelatorioController : Controller
{
    private TesteDevCartsysContext _context;
    private IMapper _mapper;
    private readonly PdfService _pdfService;
    private readonly ICompositeViewEngine _viewEngine;
    public RelatorioController(TesteDevCartsysContext context, IMapper mapper, PdfService pdfService,ICompositeViewEngine viewEngine)
    {
        _context = context;
        _mapper = mapper;
        _pdfService = pdfService;
        _viewEngine = viewEngine;
    }

    [HttpGet]
    private async Task<string> RenderViewToStringAsync(string viewName, object model)
    {
        ViewData.Model = model;
        using (var sw = new StringWriter())
        {
            var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
            if (viewResult.View == null)
            {
                throw new ArgumentNullException($"{viewName} não foi encontrada.");
            }

            var viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                ViewData,
                TempData,
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return sw.ToString();
        }
    }

    [HttpGet]
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

        var htmlContent = await RenderViewToStringAsync("Views/Relatorio.cshtml", linhasRelatorio);

        var pdfBytes = _pdfService.GeneratePdf(htmlContent);

        return File(pdfBytes, "application/pdf", "Relatorio.pdf");
    }
}

