using HSZF_09_Practice.BusinessLogic.Interfaces;
using HSZF_09_Practice.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HSZF_09_Practice.BusinessLogic.Infrastructure
{
    public static class DependencyRegister
    {
        public static void RegisterCustomServices(IServiceCollection services)
        {
            DataAccess.Infrastructure.DependencyRegister.RegisterCustomServices(services);

            // Register here your BL related interface pairs into IoC
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IRoleService, RoleService>();
        }
    }
}
