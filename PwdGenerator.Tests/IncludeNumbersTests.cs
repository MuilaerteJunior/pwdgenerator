using PwdGenerator.Core.Models;

namespace PwdGenerator.Tests
{
    public class IncludeNumbersTests
    {
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
    }
}
