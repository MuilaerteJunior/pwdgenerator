using PwdGenerator.Core;
using PwdGenerator.Core.Models;

namespace PwdGenerator.Tests
{
    public class CoreTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(true, true, true, 8)]
        [TestCase(true, false, false,10)]
        [TestCase(false, true, false, 8)]
        [TestCase(false, false, true,16)]
        [TestCase(false, false, false,20)]
        [TestCase(true, true, false,36)]
        [TestCase(false, true, true,40)]
        [TestCase(true, false, true,50)]
        public void VerifyGeneration(bool includeNumbers, bool includeSpecialChar, bool includeUpperCase, int length)
        {

            var config = new ConfigModel
            {
                IncludeNumbers = includeNumbers,
                IncludeSpecialCharacters = includeSpecialChar,
                IncludeUppercase = includeUpperCase,
                Length = length
            };

            string result = Core.PwdGenerator.Generate(config);

            Assert.That(result.Any(c => char.IsNumber(c)), Is.EqualTo(includeNumbers));
            Assert.That(result.Any(c => char.IsUpper(c)), Is.EqualTo(includeUpperCase));
            Assert.That(result.Length == length);
        }
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(50)]
        public void VerifyPwdGeneratorLength(int length)
        {
            var config = new ConfigModel
            {
                Length = length
            };

            string result = Core.PwdGenerator.Generate(config);
            Assert.That(result.Length == length);
        }

        [Test]
        public void VerifyIncludeNumbersMax()
        {

            var config = new ConfigModel
            {
                Length = 301
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => Core.PwdGenerator.Generate(config));
        }
        [Test]
        public void VerifyIncludeNumbers()
        {

            var config = new ConfigModel
            {
                IncludeNumbers = true,
                Length = 8
            };

            string result = Core.PwdGenerator.Generate(config);
            Assert.That(result.Any(c => char.IsNumber(c)));
        }
        //[Test]
        //public void VerifyIncludeSpecialChars()
        //{

        //    var config = new ConfigModel
        //    {
        //        IncludeSpecialCharacters = true,
        //        Length = 8
        //    };

        //    string result = PwdGeneratore.Generate(config);
        //    Assert.That(result.Any(c => char.(c)));
        //}
        [Test]
        public void VerifyIncludeUppercase()
        {

            var config = new ConfigModel
            {
                IncludeUppercase = true,
                Length = 8
            };

            string result = Core.PwdGenerator.Generate(config);
            Assert.That(result.Any(c => char.IsUpper(c)));

        }
    }
}
