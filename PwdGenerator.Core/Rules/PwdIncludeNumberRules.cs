
using System.Text;

namespace PwdGenerator.Core.Rules
{
    public class PwdIncludeNumberRules : PasswordCharacteristic, IPasswordCharacteristic
    {
        private const string RANGE_CHARS = "0123456789";

        public string Generate(int ruleCount)
        {
            return base.Generate(RANGE_CHARS, ruleCount);
        }
    }
}
