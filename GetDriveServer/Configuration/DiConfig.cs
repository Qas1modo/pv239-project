using AutoMapper;
using BL.Services;
using DAL;
using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork;
using DAL.UnitOfWork.Interface;
using GetDrive.Services;
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
            services.AddScoped<IRepository<Ride>, Repository<Ride>>();
            services.AddScoped<IRepository<UserRide>, Repository<UserRide>>();

            // UnitOfWork DI Setup
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Context DI Setup
            services.AddScoped<GetDriveDbContext, GetDriveDbContext>();

            //Services DI Setup
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRideService, RideService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IUserRideService, UserRideService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGeocodingService, GeocodingService>();
        }
    }
}
