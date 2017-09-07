using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
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
                XFont font = new XFont("Times", 11, XFontStyle.Bold);                
                XPen pen = new XPen(XColor.FromKnownColor(XKnownColor.Black));

                PdfPage page = document.AddPage();
                page.Size = PageSize.A4;
                page.Orientation = PageOrientation.Portrait;

                XGraphics gfx = XGraphics.FromPdfPage(page);
                XTextFormatter tf = new XTextFormatter(gfx);

                var template = new TemplateA(60);                

                gfx.DrawRectangle(pen, XBrushes.White, template.Adress.Area);
                gfx.DrawRectangle(pen, XBrushes.White, template.Sender);
                gfx.DrawRectangle(pen, XBrushes.White, template.Text);
                gfx.DrawRectangle(pen, XBrushes.White, template.LetterHead);

                gfx.DrawRectangle(pen, XBrushes.White, template.FoldMarkTop);
                gfx.DrawRectangle(pen, XBrushes.White, template.HoleMark);
                gfx.DrawRectangle(pen, XBrushes.White, template.FoldMarkButtom);

                gfx.DrawRectangle(pen, XBrushes.White, template.Adress.ReturnInformation);
                gfx.DrawRectangle(pen, XBrushes.White, template.Adress.InfoZone);
                gfx.DrawRectangle(pen, XBrushes.White, template.Adress.AdressZone);

                tf.DrawString("Rücksende Angabe", font, XBrushes.Black, template.Adress.ReturnInformation);
                tf.DrawString("Vermerkzone", font, XBrushes.Black, template.Adress.InfoZone);
                tf.DrawString("Adresszone", font, XBrushes.Black, template.Adress.AdressZone);

                tf.DrawString("Text Inhalt", font, XBrushes.Black, template.Text);
                tf.DrawString("Briefkopf", font, XBrushes.Black, template.LetterHead);
                tf.DrawString("Absender", font, XBrushes.Black, template.Sender);

                document.Save(stream);
            }

            return stream.ToArray();
        }
          
    }

    public class TemplateA
    {
        //x = Breite
        //y = Höhe
        public XRect LetterHead { get; } = new XRect(0,0, XUnit.FromMillimeter(125), XUnit.FromMillimeter(27));        
        public AdressField Adress { get; }
      
        public XRect FoldMarkTop { get; } = new XRect(XUnit.FromMillimeter(0) , XUnit.FromMillimeter(87) , XUnit.FromMillimeter(10), 1);
        public XRect HoleMark { get; } = new XRect(XUnit.FromMillimeter(0), XUnit.FromMillimeter(148.5), XUnit.FromMillimeter(10),1);
        public XRect FoldMarkButtom { get; } = new XRect(XUnit.FromMillimeter(0), XUnit.FromMillimeter(87+105), XUnit.FromMillimeter(10), 1);

        public XRect Sender { get;} 
        public XRect Text { get; } 

        public TemplateA():this(40)
        {
        }

        public TemplateA(double rightTopBoxHeight)
        {
            this.Sender  = new XRect(XUnit.FromMillimeter(125), XUnit.FromMillimeter(32), XUnit.FromMillimeter(75), XUnit.FromMillimeter(rightTopBoxHeight)); 
            this.Text = new XRect(XUnit.FromMillimeter(25), this.Sender.Bottom + XUnit.FromMillimeter(8.46), XUnit.FromMillimeter(165), XUnit.FromMillimeter(196.54)); //ende
            this.Adress = new AdressField();           
        }

        public class AdressField
        {
            public XRect Area { get; } = new XRect(XUnit.FromMillimeter(20), XUnit.FromMillimeter(27), XUnit.FromMillimeter(85), XUnit.FromMillimeter(45));
            /// <summary>
            /// Rücksende Angabe.
            /// </summary>
            public XRect ReturnInformation { get; }
            /// <summary>
            /// Vermerkzone.
            /// </summary>
            public XRect InfoZone { get; }
            /// <summary>
            /// Adresszone.
            /// </summary>
            public XRect AdressZone { get; }            

            public AdressField()
            {
                var offset = this.Area.X + XUnit.FromMillimeter(5);
                var width = this.Area.Width - XUnit.FromMillimeter(5);

                this.ReturnInformation = new XRect(offset, this.Area.Y, width, XUnit.FromMillimeter(5));
                this.InfoZone = new XRect(offset, this.ReturnInformation.Bottom, width, XUnit.FromMillimeter(12.7));
                this.AdressZone = new XRect(offset, this.InfoZone.Bottom, width, XUnit.FromMillimeter(27.3));
            }
        }
    }
}
