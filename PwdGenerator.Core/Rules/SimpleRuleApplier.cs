namespace PwdGenerator.Core.Rules
{
    public abstract class SimpleRuleApplier
    {
        protected static Random random = Random.Shared;
        protected int GetIndex(int length) => random.Next(length);
    }
}
