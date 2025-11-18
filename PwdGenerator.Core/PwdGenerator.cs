using PwdGenerator.Core.Models;
using PwdGenerator.Core.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PwdGenerator.Core
{
    public class PwdGenerator
    {
        private const string Chars = "abcdefghijklmnopqrstuvwxyz";
        private static Random random = Random.Shared;

        private static readonly List<(Func<ConfigModel, short> Predicate, Func<IPasswordCharacteristic> Factory)> RuleRegistry = new();

        static PwdGenerator()
        {
            RegisterRule(cfg => cfg.SpecialCharsCount, () => new PwdIncludeSpecialCharactersRule());
            RegisterRule(cfg => cfg.UppercaseCount, () => new PwdIncludeUppercaseRule());
            RegisterRule(cfg => cfg.NumbersCount, () => new PwdIncludeNumberRules());
        }

        public static void RegisterRule(Func<ConfigModel, short> predicate, Func<IPasswordCharacteristic> factory)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            RuleRegistry.Add((predicate, factory));
        }

        private static string GenerateRandomString(int length)
        {
            if (length > 300)
                throw new ArgumentOutOfRangeException(nameof(length), "Length exceeds maximum allowed value (300).");

            var finalString = new StringBuilder();
            for (var index = 0; index < length; index++) {
                var charIndex = random.Next(Chars.Length);
                finalString.Append(Chars[charIndex]);
            }
            return finalString.ToString();
        }

        public static string Generate(ConfigModel config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (config.Length <= 0) throw new NotSupportedException();
            if (config.UppercaseCount > config.Length) throw new NotSupportedException();
            if (config.SpecialCharsCount > config.Length) throw new NotSupportedException();
            if (config.NumbersCount > config.Length) throw new NotSupportedException();
            if (InvalidConfig(config)) throw new NotSupportedException("Options defined are higher than password length!");

            var firstPasswordVersion = GenerateRandomString(config.Length);

            var newChars = new StringBuilder();
            foreach (var (predicate, factory) in RuleRegistry)
            {
                var applier = factory();
                newChars.Append(applier.Generate(predicate(config)));
            }
            
            if (newChars.Length > 0)
                return RuleApplier.Apply(firstPasswordVersion, newChars.ToString());
            
            return firstPasswordVersion;
        }

        public static bool InvalidConfig(ConfigModel config)
        {
            return (config.UppercaseCount + config.SpecialCharsCount + config.NumbersCount > config.Length);
        }
    }
}
