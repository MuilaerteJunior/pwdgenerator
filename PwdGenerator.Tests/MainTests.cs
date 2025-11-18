using PwdGenerator.Core;
using PwdGenerator.Core.Models;

namespace PwdGenerator.Tests
{
    public class MainTests
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


        [TestCase(1, 1, 1, 8, 300)]
        [TestCase(1, 0, 0, 10, 300)]
        [TestCase(0, 1, 0, 8, 300)]
        [TestCase(0, 0, 1, 16, 300)]
        [TestCase(0, 0, 0, 20, 300)]
        [TestCase(1, 1, 0, 36, 300)]
        [TestCase(0, 1, 1, 40, 300)]
        [TestCase(1, 0, 1, 50, 300)]
        public void VerifyLongTermGeneration(short includeNumbers, short includeSpecialChar, short includeUpperCase, int length, int count)
        {

            var config = new ConfigModel
            {
                NumbersCount = includeNumbers,
                SpecialCharsCount = includeSpecialChar,
                UppercaseCount = includeUpperCase,
                Length = length
            };
            
            Parallel.For(0, count, (i) =>
            {
                string result = Core.PwdGenerator.Generate(config);
                Assert.That(result.Count(c => char.IsNumber(c)), Is.EqualTo(includeNumbers));
                Assert.That(result.Count(c => char.IsUpper(c)), Is.EqualTo(includeUpperCase));
                Assert.That(result.Length == length);
            });
        }
    }
}
