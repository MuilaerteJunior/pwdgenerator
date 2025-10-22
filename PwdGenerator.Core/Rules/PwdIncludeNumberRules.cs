
namespace PwdGenerator.Core.Rules
{
    public class PwdIncludeNumberRules: SimpleRuleApplier, IPwdSimpleRule
    {
        public string Apply(string input, short ruleCount)
        {
            var numberIndexes = GetIndexes(input.Length, ruleCount);
            var numbers = GenerateContent(ruleCount);
            return ReplaceCharAt(input, numberIndexes, numbers);
        }

        protected override Stack<char> GenerateContent(short ruleCount)
        {
            var finalNumbers = new Stack<char>();
            for (int i = 0; i < ruleCount; i++)
            {
                finalNumbers.Push(random.Next(9).ToString().First());
            }
            return finalNumbers;
        }
    }
}
