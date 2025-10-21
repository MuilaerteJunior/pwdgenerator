namespace PwdGenerator.Core.Rules
{
    public class PwdIncludeSpecialCharactersRule : SimpleRuleApplier, IPwdSimpleRule
    {
        private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;':\",./<>?`~";
        public string Apply(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var index = GetIndex(input.Length);
            var specialChar = SpecialChars[random.Next(SpecialChars.Length)];
            var chars = input.ToCharArray();
            chars[index] = specialChar;
            return new string(chars);
        }
    }
}
