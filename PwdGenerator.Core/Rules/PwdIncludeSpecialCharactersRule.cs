
namespace PwdGenerator.Core.Rules
{
    public class PwdIncludeSpecialCharactersRule : SimpleRuleApplier, IPwdSimpleRule
    {
        private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;':\",./<>?`~";
        public string Apply(string input, short ruleCount)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var indexes = GetIndexes(input.Length, ruleCount);
            var specialChars = GenerateContent(ruleCount);
            return ReplaceCharAt(input, indexes, specialChars);
        }

        protected override Stack<char> GenerateContent(short ruleCount)
        {
            var finalSpecialChars = new Stack<char>();
            for (int i = 0; i < ruleCount; i++)
            {
                finalSpecialChars.Push(SpecialChars[_random.Next(SpecialChars.Length)]);
            }
            return finalSpecialChars;
        }
    }
}
