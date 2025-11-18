using PwdGenerator.Core.Models;

namespace PwdGenerator.Tests
{
    public class SpecialCharsTests
    {
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

    }
}
