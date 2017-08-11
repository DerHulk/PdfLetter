using System;
using System.Collections.Generic;
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
            target.Write();

            //assert
            Assert.True(true);
        }
    }
}
