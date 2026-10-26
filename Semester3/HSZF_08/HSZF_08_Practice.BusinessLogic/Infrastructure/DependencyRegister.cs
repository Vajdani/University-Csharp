using HSZF_08_Practice.BusinessLogic.Interfaces;
using HSZF_08_Practice.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HSZF_08_Practice.BusinessLogic.Infrastructure
{
    public static class DependencyRegister
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Register DA
            DataAccess.Infrastructure.DependencyRegister.RegisterServices(services);

            //Register BL
            services.AddScoped<IMovieService,  MovieService>();
            services.AddScoped<IRoleService,   RoleService>();
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}
