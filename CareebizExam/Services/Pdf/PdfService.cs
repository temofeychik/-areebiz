using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;

namespace CareebizExam.WebApi.Services.Pdf
{
    public class PdfService : IPdfService
    {
        public Stream ImageToPdf(Stream imageStream, int pageWidth)
        {
            Stream stream = default;
            using (var document = new PdfDocument())
            {
                var page = document.AddPage();

                using (XImage img = XImage.FromStream(() => { return imageStream; }))
                {
                    var height = img.PixelHeight;

                    // Change PDF Page size to match image
                    page.Width = pageWidth;
                    page.Height = height;

                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    gfx.DrawImage(img, 0, 0, pageWidth, height);
                }
                stream = new MemoryStream();
                
                document.Save(stream);

                stream.Position = 0;
            }

            return stream;
        }
    }
}
