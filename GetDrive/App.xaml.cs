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

        protected override void OnStart()
        {
            base.OnStart();

            var globalExceptionServiceInitializer = serviceProvider.GetRequiredService<IGlobalExceptionServiceInitializer>();
            globalExceptionServiceInitializer.Initialize();
        }
    }
}
