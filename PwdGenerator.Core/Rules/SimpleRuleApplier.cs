namespace PwdGenerator.Core.Rules
{
    public abstract class SimpleRuleApplier
    {
        protected static Random random = Random.Shared;
        protected abstract Stack<char> GenerateContent(short ruleCount);
        protected IEnumerable<int> GetIndexes(int length, short ruleCount)
        {
            if (ruleCount > length) throw new NotSupportedException("RuleCount > length");
            if (ruleCount == length) return Enumerable.Range(0, length).ToArray();

            var finalIndexes = new HashSet<int>();
            while (finalIndexes.Count < ruleCount)
            {
                var index = random.Next(length);
                if (!finalIndexes.Contains(index))
                    finalIndexes.Add(index);
            }
            return finalIndexes;
        }

        protected string ReplaceCharAt(string input, IEnumerable<int> indexes, Stack<char> newChar)
        {
            if (indexes == null || !indexes.Any())
                return input;
            if (indexes != null && indexes.Any(i => i >= input.Length))
                throw new ArgumentOutOfRangeException(nameof(indexes), "Index is out of range.");

            var charArray = input.ToCharArray();
            foreach (var index in indexes)
                charArray[index] = newChar.Pop();
            return new string(charArray);
        }

        protected string UppercaseCharsAt(string input, IEnumerable<int> indexes)
        {
            if (indexes == null || !indexes.Any())
                return input;
            if (indexes != null && indexes.Any(i => i >= input.Length))
                throw new ArgumentOutOfRangeException(nameof(indexes), "Index is out of range.");

            var charArray = input.ToCharArray();
            foreach (var index in indexes)
                charArray[index] = char.ToUpper(charArray[index]);
            return new string(charArray);
        }
    }
}
