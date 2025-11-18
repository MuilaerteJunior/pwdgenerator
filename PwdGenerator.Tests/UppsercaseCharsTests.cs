using PwdGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwdGenerator.Tests
{
    public class UppsercaseCharsTests
    {

        [TestCase(2)]
        public void VerifyIncludeUppercase(short upperCaseCount)
        {

            var config = new ConfigModel
            {
                UppercaseCount = upperCaseCount,
                Length = 8
            };

            string result = Core.PwdGenerator.Generate(config);
            Assert.That(result.Count(c => char.IsUpper(c)), Is.EqualTo(upperCaseCount));

        }
    }
}
