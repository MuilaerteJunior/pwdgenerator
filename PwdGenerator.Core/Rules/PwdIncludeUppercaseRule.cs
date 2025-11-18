using System;

namespace PwdGenerator.Core.Rules
{
    public class PwdIncludeUppercaseRule : PasswordCharacteristic, IPasswordCharacteristic
    {
        private const string CAPITAL_CHARS = "ABCDEFGHIJKLMNOPQRSTUWVXYZ";
        public string Generate(int ruleCount)
        {
            return base.Generate(CAPITAL_CHARS, ruleCount);
        }
    }
}
