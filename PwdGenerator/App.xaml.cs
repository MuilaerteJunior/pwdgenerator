namespace PwdGenerator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());
            window.Height = 300;
            window.Width = 1200;
            return window;
        }
    }
}