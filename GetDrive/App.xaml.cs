using GetDrive.Services;

namespace GetDrive
{
    public partial class App : Application
    {
        private readonly IServiceProvider serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            InitializeComponent();

            MainPage = serviceProvider.GetRequiredService<AppShell>();
        }
    }
}
