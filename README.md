# Password generator

## 🔐 Customizable Password Generator (MAUI)
A cross-platform password generation utility built with .NET MAUI. This application allows users to create strong, customized passwords by selecting the desired length and character types, ensuring robust security for various needs.


## ✨ Features
Customizable Length: Easily select the password length using a dedicated Picker component (from 6 to 50 characters).

Character Sets: Options to include uppercase letters, lowercase letters, numbers, and symbols.

Cross-Platform: Runs natively on Windows, macOS, Android, and iOS using the .NET MAUI framework.

Instant Generation: Quickly generate and copy passwords to the clipboard.

<hr>

## 🛠️ Technology Stack
Framework: .NET MAUI

Language: C#

UI: XAML

<hr>

## ⚙️ Getting Started
Prerequisites
.NET 8 SDK or newer.

Visual Studio 2022 (with the .NET Multi-platform App UI development workload installed).

Installation and Run
Clone the Repository:

```Bash
git clone https://github.com/MuilaerteJunior/pwdgenerator.git
cd PwdGenerator
Open in Visual Studio: Open the PwdGenerator.sln file in Visual Studio 2022.
```

Run the Project: Select your desired target platform (e.g., Windows Machine, Android Emulator) from the dropdown menu and press F5 to run the application.

## 📝 CLI Commands

 1. CLI behavior:
    - Accept command line options: --length <int>, --uppercase <short>, --special <short>, --numbers <short>, --interactive, --help
    - If --interactive is specified or no options given, prompt user for values interactively with defaults
    - Validate numeric inputs and ensure they are within logical bounds (non-negative, counts <= length)
    - Build a ConfigModel and call PwdGenerator.Generate(config)
    - Print the generated password to the console
    - Return non-zero exit codes for invalid input or exceptions
 2. Example usage:
    - dotnet run -- --length 16 --uppercase 3 --special 2 --numbers 2
    - dotnet run -- --interactive

## Release notes

- 1.0: Initial release with basic password generation features.

- 1.1: Update to allow to inform the number of uppercase, special characters and numbers in the password.

<hr>

## ⚖️ License and Usage Restrictions
This project is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International (CC BY-NC-ND 4.0) License.

This license permits the following:

✅ Private Use: You are free to download, install, and use this software for your own personal, private, and non-commercial purposes.

✅ Attribution: You must give appropriate credit.

This license explicitly forbids the following:

🚫 Commercial Use: You may not use this software for any commercial purpose, including selling it, generating revenue from it, or incorporating it into a commercial product or service.

🚫 Distribution: You may not distribute the original or modified code.

🚫 Derivatives: You may not share or distribute modified versions of this code (i.e., you cannot build upon it and distribute the changes).

For commercial inquiries, please contact the author.