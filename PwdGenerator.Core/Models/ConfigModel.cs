namespace PwdGenerator.Core.Models
{
    public class ConfigModel
    {
        public short UppercaseCount { get; set; }
        public short SpecialCharsCount { get; set; }
        public short NumbersCount { get; set; }
        public int Length { get; set; }
    }
}
