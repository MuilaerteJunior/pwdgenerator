using PwdGenerator.Core;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace PwdGenerator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            lengthPicker.SelectedIndex = 0;
            specialCharsPicker.SelectedIndex = 0;
            numbersPicker.SelectedIndex = 1;
            capitalsPicker.SelectedIndex = 1;

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
            var result = Core.PwdGenerator.Generate(config);
            entryPwd.Text = result;
        }

        private async void BtnCopy_Click(object sender, EventArgs e)
        {
            var textToCopy = entryPwd?.Text ?? string.Empty;
            if (string.IsNullOrEmpty(textToCopy))
                return;

            await Clipboard.Default.SetTextAsync(textToCopy);
            await DisplayAlert("Copied", "Password copied to clipboard.", "OK");
        }
    }
}
