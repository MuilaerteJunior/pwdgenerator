namespace PwdGenerator.Core.Rules
{
    public interface IPasswordCharacteristic
    {
        string Generate(int ruleCount);

    }
}
