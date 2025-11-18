using PwdGenerator.Core.Models;

namespace PwdGenerator.Tests
{
    public class IntegrityTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1, 1, 1, 8, 300)]
        [TestCase(1, 0, 0, 10, 300)]
        [TestCase(0, 1, 0, 8, 300)]
        [TestCase(0, 0, 1, 16, 300)]
        [TestCase(0, 0, 0, 20, 300)]
        [TestCase(1, 1, 0, 36, 300)]
        [TestCase(0, 1, 1, 40, 30000)]
        [TestCase(1, 0, 1, 50, 3000)]
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
