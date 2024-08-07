using DinkToPdf;
using DinkToPdf.Contracts;
using RazorLight;
using System.IO;
using System.Threading.Tasks;
using TesteDevCartsys.Views;

namespace TesteDevCartsys.Services
{
    public class PdfService
    {
        private readonly IRazorLightEngine _razorLightEngine;
        private readonly IConverter _converter;

        public PdfService(IRazorLightEngine razorLightEngine, IConverter converter)
        {
            _razorLightEngine = razorLightEngine;
            _converter = converter;
        }

        public async Task<byte[]> GeneratePdfAsync(List<LinhaRelatorio> linhas)
        {
            var html = await _razorLightEngine.CompileRenderAsync("Views/Relatorio.cshtml", linhas);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects = {
                new ObjectSettings
                {
                    HtmlContent = html,
                    WebSettings = { DefaultEncoding = "utf-8" },
                }
            }
            };

            return _converter.Convert(doc);
        }
    }
}
