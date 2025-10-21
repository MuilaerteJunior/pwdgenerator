namespace PwdGenerator.Core.Rules
{
    public class PwdIncludeUppercaseRule : SimpleRuleApplier, IPwdSimpleRule
    {
        public string Apply(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var index = GetIndex(input.Length);
            var chars = input.ToCharArray();
            chars[index] = char.ToUpperInvariant(chars[index]);
            return new string(chars);
        }
    }
}
