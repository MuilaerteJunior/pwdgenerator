namespace PwdGenerator.Core.Models
{
    public class ConfigModel
    {
        public bool IncludeUppercase { get; set; }
        public bool IncludeSpecialCharacters { get; set; }
        public bool IncludeNumbers { get; set; }
        public int Length { get; set; }
    }
}
