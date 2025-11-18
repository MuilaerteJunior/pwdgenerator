using System.Text;

namespace PwdGenerator.Core.Rules
{
    public abstract class PasswordCharacteristic {

        protected static readonly Random _random = Random.Shared;
        protected string Generate(string rangeCharacters, int ruleCount) {
            var finalInputs = new StringBuilder();
            for (var index = 0; index < ruleCount; index++)
            {
                finalInputs.Append(rangeCharacters[_random.Next(rangeCharacters.Length)]);

            }
            return finalInputs.ToString();
        }
    }
}
