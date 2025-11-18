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
            window.MinimumHeight = 600;
            //window.MinimumWidth = 1200;
            return window;
        }
    }
}