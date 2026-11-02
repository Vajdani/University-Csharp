using HSZF_09_Practice.WebAPI.Exceptions;

namespace HSZF_09_Practice.WebAPI.Infrastructure
{
    public static class DependencyRegister
    {
        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            BusinessLogic.Infrastructure.DependencyRegister.RegisterCustomServices(services);

            // Register here your Web related interface pairs into IoC
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
