using IronPdf;
using FYP.DB;

namespace FYP.Services
{
    public class PDF_Generator
    {
        public byte[] GetHTMLPageAsPDF(string data)
        {
            var Renderer = new IronPdf.ChromePdfRenderer();
            // Create a PDF Document
            Renderer.RenderingOptions.HtmlFooter = new HtmlHeaderFooter()
            {
                MaxHeight = 15, //millimeters
                HtmlFragment = "<center><i>{page} of {total-pages}<i></center>",
                DrawDividerLine = true
            };
            Renderer.RenderingOptions.MarginBottom = 25; //mm
            Renderer.RenderingOptions.HtmlHeader = new HtmlHeaderFooter()
            {
                MaxHeight = 20, //millimeters
                HtmlFragment = "<img src='logo.png'>",
                BaseUrl = new Uri(@"C:\assets\images\").AbsoluteUri
            };
            Renderer.RenderingOptions.MarginTop = 25; //mm

            using var PDF = Renderer.RenderHtmlAsPdf(data);

            return PDF.BinaryData;
        }
    }
}
