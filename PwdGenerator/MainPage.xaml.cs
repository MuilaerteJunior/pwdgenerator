using PwdGenerator.Core.Models;
using PwdGenerator.Resources.Strings;
using System.Text.Json;
using System.IO;
using Microsoft.Extensions.Logging;

namespace PwdGenerator
{
    public partial class MainPage : ContentPage
    {
        private const string CONFIG_FILE_PATH = "app_settings.json";

        private readonly string appDataPath = FileSystem.Current.AppDataDirectory;
        private readonly ILogger<MainPage> _logger;

        public MainPage(ILogger<MainPage> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogDebug("MainPage constructor called. AppDataPath: {AppDataPath}", appDataPath);

            InitializeComponent();

            var config = LoadConfigFile();
            if (config == null)
            {
                _logger.LogDebug("No config file found; setting default picker indices.");
                lengthPicker.SelectedIndex = 0;
                specialCharsPicker.SelectedIndex = 0;
                numbersPicker.SelectedIndex = 1;
                capitalsPicker.SelectedIndex = 1;
            }
            else
            {
                _logger.LogDebug("Loaded config from file: {@Config}", config);
                lengthPicker.SelectedItem = config.Length;
                specialCharsPicker.SelectedItem = config.SpecialCharsCount;
                numbersPicker.SelectedItem = config.NumbersCount;
                capitalsPicker.SelectedItem = config.UppercaseCount;
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            var config = new Core.Models.ConfigModel
            {
                NumbersCount = (numbersPicker.SelectedItem as short?) ?? 0,
                SpecialCharsCount = (specialCharsPicker.SelectedItem as short?) ?? 0,
                UppercaseCount = (capitalsPicker.SelectedItem as short?) ?? 0,
                Length = ((lengthPicker.SelectedItem as int?) ?? 6)
            };

            _logger.LogInformation("Generate button clicked with config: {@Config}", config);

            if (!Core.PwdGenerator.InvalidConfig(config))
            {
                try
                {
                    SaveConfigLocally(config);
                    _logger.LogDebug("Config saved locally.");
                }
                catch (Exception ex)
                {
                    // SaveConfigLocally already logs, but guard here as well.
                    _logger.LogWarning(ex, "Failed to save config locally.");
                }

                try
                {
                    var result = Core.PwdGenerator.Generate(config);
                    entryPwd.Text = result;
                    _logger.LogInformation("Password generated (length {Length}).", result?.Length ?? 0);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while generating password.");
                    DisplayAlert("Error", "An error occurred while generating the password.", "OK");
                }
            }
            else
            {
                _logger.LogWarning("Invalid config provided: {@Config}", config);
                DisplayAlert(AppResources.InvalidConfigTitle, AppResources.InvalidConfig, "OK");
            }
        }

        private ConfigModel? LoadConfigFile()
        {
            ConfigModel? config = null;
            try
            {
                var path = Path.Combine(appDataPath, CONFIG_FILE_PATH);
                _logger.LogDebug("Attemptingg to load config file at {Path}", path);

                if (!File.Exists(path))
                {
                    _logger.LogDebug("Config file does not exist at {Path}", path);
                    return null;
                }

                using (var fr = new StreamReader(path))
                {
                    var content = fr.ReadToEnd();
                    config = JsonSerializer.Deserialize<ConfigModel>(content) ?? new ConfigModel();
                    _logger.LogInformation($"Config file loaded successfully at {path}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Failed to read or deserialize config file. {ex}");
            }

            return config;
        }

        private void SaveConfigLocally(ConfigModel config)
        {
            try
            {
                var path = Path.Combine(appDataPath, CONFIG_FILE_PATH);
                using (var fw = new StreamWriter(path))
                {
                    fw.Write(JsonSerializer.Serialize(config));
                }
                _logger.LogInformation("Config saved to {Path}", Path.Combine(appDataPath, CONFIG_FILE_PATH));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save config to local storage.");
                // swallow to preserve original behavior, but logged for diagnostics
            }
        }

        private async void BtnCopy_Click(object sender, EventArgs e)
        {
            string textToCopy = entryPwd?.Text ?? string.Empty;
            if (string.IsNullOrEmpty(textToCopy))
            {
                _logger.LogDebug("Copy requested but there is no text to copy.");
                return;
            }

            try
            {
                await Clipboard.Default.SetTextAsync(textToCopy);
                _logger.LogInformation("Password copied to clipboard (length {Length}).", textToCopy.Length);
                await DisplayAlert("Copied", "Password copied to clipboard.", "OK");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to copy password to clipboard.");
                await DisplayAlert("Error", "Failed to copy to clipboard.", "OK");
            }
        }
    }
}
