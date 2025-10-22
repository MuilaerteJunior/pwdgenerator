using System;

namespace PwdGenerator.Core.Rules
{
    public class PwdIncludeUppercaseRule : SimpleRuleApplier, IPwdSimpleRule
    {
        public string Apply(string input, short ruleCount)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var indexes = GetIndexes(input.Length, ruleCount);
            var chars = input.ToCharArray();
            return UppercaseCharsAt(input, indexes);
        }

        protected override Stack<char> GenerateContent(short ruleCount)
        {
            throw new NotImplementedException();
        }
    }
}
