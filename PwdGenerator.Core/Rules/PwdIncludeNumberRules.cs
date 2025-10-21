namespace PwdGenerator.Core.Rules
{
    public class PwdIncludeNumberRules: SimpleRuleApplier, IPwdSimpleRule
    {
        public string Apply(string input)
        {
            var numberIndex = GetIndex(input.Length);
            var result = input.Replace(input[numberIndex], random.Next(9).ToString().ToCharArray()[0]);
            return result;
        }
    }
}
