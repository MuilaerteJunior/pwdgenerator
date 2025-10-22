// 2. CLI behavior:
//    - Accept command line options: --length <int>, --uppercase <short>, --special <short>, --numbers <short>, --interactive, --help
//    - If --interactive is specified or no options given, prompt user for values interactively with defaults
//    - Validate numeric inputs and ensure they are within logical bounds (non-negative, counts <= length)
//    - Build a ConfigModel and call PwdGenerator.Generate(config)
//    - Print the generated password to the console
//    - Return non-zero exit codes for invalid input or exceptions
// 4. Example usage:
//    - dotnet run -- --length 16 --uppercase 3 --special 2 --numbers 2
//    - dotnet run -- --interactive
//
// csproj (create file at PwdGenerator.Cli/PwdGenerator.Cli.csproj):
/*
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include="..\PwdGenerator.Core\PwdGenerator.Core.csproj" />
  </ItemGroup>
</Project>
*/

// Actual CLI implementation:
using PwdGenerator.Core.Models;

static void PrintHelp()
{
    Console.WriteLine("PwdGenerator.Cli - simple password generator using PwdGenerator.Core");
    Console.WriteLine();
    Console.WriteLine("Usage:");
    Console.WriteLine("  --length <int>       Password length (required unless interactive)");
    Console.WriteLine("  --uppercase <short>  Number of uppercase letters to include (default 0)");
    Console.WriteLine("  --special <short>    Number of special characters to include (default 0)");
    Console.WriteLine("  --numbers <short>    Number of numeric characters to include (default 0)");
    Console.WriteLine("  --interactive        Prompt for values interactively");
    Console.WriteLine("  --help               Show this help");
    Console.WriteLine();
    Console.WriteLine("Examples:");
    Console.WriteLine("  dotnet run -- --length 16 --uppercase 3 --special 2 --numbers 2");
    Console.WriteLine("  dotnet run -- --interactive");
}

int ExitWithError(string message, int code = 1)
{
    Console.Error.WriteLine(message);
    return code;
}

bool TryParseShort(string? s, out short value)
{
    value = 0;
    if (string.IsNullOrWhiteSpace(s)) return false;
    if (short.TryParse(s, out var v) && v >= 0) { value = v; return true; }
    return false;
}

bool TryParseInt(string? s, out int value)
{
    value = 0;
    if (string.IsNullOrWhiteSpace(s)) return false;
    if (int.TryParse(s, out var v) && v >= 0) { value = v; return true; }
    return false;
}

// Parse args:
var argsList = Environment.GetCommandLineArgs();
var arguments = argsList.Length > 1 ? argsList[1..] : Array.Empty<string>();

int? length = null;
short uppercase = 0;
short special = 0;
short numbers = 0;
bool interactive = false;

for (int i = 0; i < arguments.Length; i++)
{
    var a = arguments[i];
    switch (a)
    {
        case "--help":
        case "-h":
            PrintHelp();
            return;
        case "--interactive":
            interactive = true;
            break;
        case "--length":
            if (i + 1 >= arguments.Length) { ExitWithError("Missing value for --length"); return; }
            if (!TryParseInt(arguments[i + 1], out var l)) { ExitWithError("Invalid integer for --length"); return; }
            length = l;
            i++;
            break;
        case "--uppercase":
            if (i + 1 >= arguments.Length) { ExitWithError("Missing value for --uppercase"); return; }
            if (!TryParseShort(arguments[i + 1], out var u)) { ExitWithError("Invalid value for --uppercase"); return; }
            uppercase = u;
            i++;
            break;
        case "--special":
            if (i + 1 >= arguments.Length) { ExitWithError("Missing value for --special"); return; }
            if (!TryParseShort(arguments[i + 1], out var s)) { ExitWithError("Invalid value for --special"); return; }
            special = s;
            i++;
            break;
        case "--numbers":
            if (i + 1 >= arguments.Length) { ExitWithError("Missing value for --numbers"); return; }
            if (!TryParseShort(arguments[i + 1], out var n)) { ExitWithError("Invalid value for --numbers"); return; }
            numbers = n;
            i++;
            break;
        default:
            ExitWithError($"Unknown argument: {a}");
            return;
    }
}

// Interactive prompting if requested or if length not provided
if (interactive || length == null)
{
    try
    {
        Console.WriteLine("Interactive password generator");
        if (length == null)
        {
            while (true)
            {
                Console.Write("Password length (integer > 0): ");
                var input = Console.ReadLine();
                if (TryParseInt(input, out var l) && l > 0) { length = l; break; }
                Console.WriteLine("Please enter a positive integer.");
            }
        }

        Console.Write($"Uppercase count (current {uppercase}): ");
        var up = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(up))
        {
            if (!TryParseShort(up, out var u)) { ExitWithError("Invalid uppercase value"); return; }
            uppercase = u;
        }

        Console.Write($"Special characters count (current {special}): ");
        var sp = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(sp))
        {
            if (!TryParseShort(sp, out var s)) { ExitWithError("Invalid special value"); return; }
            special = s;
        }

        Console.Write($"Numbers count (current {numbers}): ");
        var nm = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nm))
        {
            if (!TryParseShort(nm, out var n)) { ExitWithError("Invalid numbers value"); return; }
            numbers = n;
        }
    }
    catch (Exception ex)
    {
        ExitWithError($"Interactive input failed: {ex.Message}");
        return;
    }
}

if (length == null)
{
    ExitWithError("Password length is required. Use --length or --interactive.");
    return;
}

if (length <= 0)
{
    ExitWithError("Length must be greater than zero.");
    return;
}

if (uppercase > length || special > length || numbers > length)
{
    ExitWithError("One of the requested counts exceeds the total length.");
    return;
}

if ((int)uppercase + (int)special + (int)numbers > length)
{
    ExitWithError("Sum of uppercase, special and numbers exceeds total length.");
    return;
}

// Build config and generate
try
{
    var cfg = new ConfigModel
    {
        Length = length.Value,
        UppercaseCount = uppercase,
        SpecialCharsCount = special,
        NumbersCount = numbers
    };

    var pwd = PwdGenerator.Core.PwdGenerator.Generate(cfg);
    Console.WriteLine();
    Console.WriteLine("Generated password:");
    Console.WriteLine(pwd);
}
catch (Exception ex)
{
    ExitWithError($"Error generating password: {ex.Message}");
    return;
}