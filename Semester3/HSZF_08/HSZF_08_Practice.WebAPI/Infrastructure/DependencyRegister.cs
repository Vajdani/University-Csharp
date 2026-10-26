using HSZF_08_Practice.BusinessLogic.Interfaces;
using HSZF_08_Practice.BusinessLogic.Services;

namespace HSZF_08_Practice.WebAPI.Infrastructure
{
    public static class DependencyRegister
    {
        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            BusinessLogic.Infrastructure.DependencyRegister.RegisterServices(services);

            return services;
        }
    }
}
