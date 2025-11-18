
using System;

namespace PwdGenerator.Core.Rules
{

    public class PwdIncludeSpecialCharactersRule : PasswordCharacteristic, IPasswordCharacteristic
    {
        private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;':\",./<>?`~";

        public string Generate(int ruleCount)
        {
            return base.Generate(SpecialChars, ruleCount);
        }
    }
}
