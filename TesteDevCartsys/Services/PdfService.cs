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
        private readonly IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratePdf(string htmlContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
            },
                Objects = {
                new ObjectSettings() {
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                },
            }
            };

            return _converter.Convert(doc);
        }
    }

}
