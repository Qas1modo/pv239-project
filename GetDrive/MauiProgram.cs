using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using GetDrive.Clients;
using System.Reflection;
using GetDrive.Api;
using Microsoft.Maui.Controls;
using GetDrive.Resources.Fonts;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using GetDrive.ViewModels;
using GetDrive.Views;

namespace GetDrive
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FontAwesome-Solid.ttf", Fonts.FontAwesome);
                    fonts.AddFont("Montserrat-Bold.ttf", Fonts.Bold);
                    fonts.AddFont("Montserrat-Medium.ttf", Fonts.Medium);
                    fonts.AddFont("Montserrat-Regular.ttf", Fonts.Regular);
                });
            ConfigureAppSettings(builder);
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            ConfigureApiClients(builder.Services, builder.Configuration);
            ConfigureViewModels(builder.Services);
            ConfigureViews(builder.Services);
            builder.Services.AddAutoMapper(typeof(MauiProgram));
            return builder.Build();
        }

        private static void ConfigureAppSettings(MauiAppBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var configurationBuilder = new ConfigurationBuilder();
            var appSettingsStream = assembly.GetManifestResourceStream("GetDrive.Configuration.appsettings.json");
            if (appSettingsStream is not null)
            {
                 configurationBuilder.AddJsonStream(appSettingsStream);
            }
            builder.Configuration.AddConfiguration(configurationBuilder.Build());
        }

        private static void ConfigureApiClients(IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient<Client>();
            services.AddTransient<IAuthClient, AuthClient>();
            services.AddTransient<IReviewClient, ReviewClient>();
            services.AddTransient<IRideClient, RideClient>();
            services.AddTransient<IUserClient, UserClient>();
            services.AddTransient<IUserRideClient, UserRideClient>();
        }

        private static void ConfigureViewModels(IServiceCollection services)
        {
            services.Scan(selector => selector
                .FromAssemblyOf<App>()
                .AddClasses(filter => filter.AssignableTo<ViewModelBase>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());
        }

        private static void ConfigureViews(IServiceCollection services)
        {
            services.Scan(selector => selector
                .FromAssemblyOf<App>()
                .AddClasses(filter => filter.AssignableTo<ContentPageBase>())
                .AsSelf()
                .WithTransientLifetime());
        }
    }
}
