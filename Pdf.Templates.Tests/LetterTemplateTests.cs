using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pdf.Templates.Tests
{
    public class LetterTemplateTests
    {
        [Fact]
        public void Write01()
        {
            //arrange
            var target = new LetterTemplate();

            //act
            var result = target.Write();
            var fileName = System.IO.Path.GetTempFileName() + ".pdf";

            System.IO.File.WriteAllBytes(fileName, result);

            //assert                
            Process.Start(fileName);
            System.IO.File.Delete(fileName);

        }
    }
}
