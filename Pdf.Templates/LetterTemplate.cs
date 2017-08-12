using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Pdf.Templates
{
    public class LetterTemplate
    {
        public byte[] Write()
        {
            var stream = new MemoryStream();

            using (var document = new PdfDocument())
            {
                XFont font = new XFont("Times", 25, XFontStyle.Bold);

                PdfPage page = document.AddPage();
                page.Size = PageSize.A4;

                XGraphics gfx = XGraphics.FromPdfPage(page);

                gfx.DrawString("Hallo Welt", font, XBrushes.DarkRed, new XRect(0, 0, page.Width, page.Height), XStringFormat.Center);

                document.Save(stream);
            }

            return stream.ToArray();
        }
    }
}
