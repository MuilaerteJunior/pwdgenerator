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

        [TestCase(1, 1, 1, 8)]
        [TestCase(1, 0, 0,10)]
        [TestCase(0, 1, 0, 8)]
        [TestCase(0, 0, 1,16)]
        [TestCase(0, 0, 0,20)]
        [TestCase(1, 1, 0,36)]
        [TestCase(0, 1, 1,40)]
        [TestCase(1, 0, 1,50)]
        public void VerifyGeneration(short includeNumbers, short includeSpecialChar, short includeUpperCase, int length)
        {

            var config = new ConfigModel
            {
                NumbersCount = includeNumbers,
                SpecialCharsCount = includeSpecialChar,
                UppercaseCount = includeUpperCase,
                Length = length
            };

            string result = Core.PwdGenerator.Generate(config);

            Assert.That(result.Count(c => char.IsNumber(c)), Is.EqualTo(includeNumbers));
            Assert.That(result.Count(c => char.IsUpper(c)), Is.EqualTo(includeUpperCase));
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
        [TestCase(4)]
        public void VerifyIncludeNumbers(short numbersCount)
        {

            var config = new ConfigModel
            {
                NumbersCount = numbersCount,
                Length = 8
            };

            string result = Core.PwdGenerator.Generate(config);
            Assert.That(result.Count(c => char.IsNumber(c)), Is.EqualTo(numbersCount));
        }
        [TestCase(3)]
        public void VerifyIncludeSpecialChars(short specialCharsCount)
        {
            var specialChars = "!@#$%^&*()_+-=[]{}|;':\",./<>?`~";
            var config = new ConfigModel
            {
                SpecialCharsCount = specialCharsCount,
                Length = 8
            };

            string result = Core.PwdGenerator.Generate(config);
            Assert.That(result.Count(c => specialChars.Contains(c)), Is.EqualTo(specialCharsCount));
        }

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
