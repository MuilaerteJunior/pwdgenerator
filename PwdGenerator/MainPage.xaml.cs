using PwdGenerator.Core;

namespace PwdGenerator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            lengthPicker.SelectedIndex = 0;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var config = new Core.Models.ConfigModel
            {
                IncludeNumbers = IncludeNumbers.IsChecked,
                IncludeSpecialCharacters = IncludeSpecialChars.IsChecked,
                IncludeUppercase = IncludeCapitals.IsChecked,
                Length = ((lengthPicker.SelectedItem as int?) ?? 6)
            };
            var result = Core.PwdGenerator.Generate(config);
            entryPwd.Text = result;
        }
    }
}
