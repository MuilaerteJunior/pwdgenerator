using PwdGenerator.Core;

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

        private void Button_Clicked(object sender, EventArgs e)
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
    }
}
