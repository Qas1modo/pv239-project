using AutoMapper;
using BL.Services.HoldingService;
using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork;
using DAL.UnitOfWork.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration
{
    public class DiConfig
    {
        public static void ConfigureDi(IServiceCollection services)
        {
            // Mapping DI Setup
            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)));

            //Repository DI Setup
            services.AddScoped<IRepository<Review>, Repository<Review>>();
            services.AddScoped<IRepository<User>, Repository<User>>();

            // UnitOfWork DI Setup
            services.AddScoped<IUoWReview, UoWReview>();

            //Services DI Setup
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
