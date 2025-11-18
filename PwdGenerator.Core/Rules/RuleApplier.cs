namespace PwdGenerator.Core.Rules
{
    public abstract class RuleApplier
    {
        private static readonly Random _random = Random.Shared;
        private static IEnumerable<int> GetIndexes(int length, int ruleCount)
        {
            if (ruleCount > length) throw new NotSupportedException("RuleCount > length");
            if (ruleCount == length) return [.. Enumerable.Range(0, length)];

            var finalIndexes = new HashSet<int>();
            while (finalIndexes.Count < ruleCount)
            {
                var index = _random.Next(length);
                if (!finalIndexes.Contains(index))
                    finalIndexes.Add(index);
            }
            return finalIndexes;
        }

        private static string ReplaceCharAt(string input, IEnumerable<int> indexes, Stack<char> newChar)
        {
            if (indexes == null || !indexes.Any())
                return input;
            if (indexes != null && indexes.Any(i => i >= input.Length))
                throw new ArgumentOutOfRangeException(nameof(indexes), "Index is out of range.");

            var charArray = input.ToCharArray();
            foreach (var index in indexes!)
                charArray[index] = newChar.Pop();
            return new string(charArray);
        }


        public static string Apply(string originalInput, string newChars)
        {
            var newCharsStack = new Stack<char>(newChars);
            var indexes = GetIndexes(originalInput.Length, newChars.Length);
            var withReplacedChars = ReplaceCharAt(originalInput, indexes, newCharsStack);
            return withReplacedChars;
        }
    }
}
