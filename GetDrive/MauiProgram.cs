using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using GetDrive.Clients;
using System.Reflection;
using GetDrive.Api;

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
                });
            ConfigureAppSettings(builder);
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            ConfigureApiClients(builder.Services, builder.Configuration);
            builder.Services.AddAutoMapper(typeof(MauiProgram));
            return builder.Build();
        }

        private static void ConfigureAppSettings(MauiAppBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            builder.Configuration.AddConfiguration(configuration);
        }

        private static void ConfigureApiClients(IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient<Client>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("Server").GetSection("Host").Value ??
                    throw new Exception("Server host url is missing in configuration!"));
            });
            services.AddTransient<IAuthClient, AuthClient>();
            services.AddTransient<IReviewClient, ReviewClient>();
            services.AddTransient<IRideClient, RideClient>();
            services.AddTransient<IUserClient, UserClient>();
            services.AddTransient<IUserRideClient, UserRideClient>();
        }
    }
}
