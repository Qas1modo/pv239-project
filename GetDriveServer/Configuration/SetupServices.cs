using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration
{
    public class SetupServices
    {
        public static void AddServices(IServiceCollection services, IConfigurationManager config)
        {
            services.AddDbContext<GetDriveDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlite(config.GetConnectionString("SqlLite"), b => b.MigrationsAssembly("GetDriveServer"));
            });
        }
    }
}
