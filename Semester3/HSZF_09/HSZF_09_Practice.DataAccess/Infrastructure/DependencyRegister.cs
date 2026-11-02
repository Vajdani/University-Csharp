using HSZF_09_Practice.DataAccess.Contexts;
using HSZF_09_Practice.DataAccess.Interfaces;
using HSZF_09_Practice.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HSZF_09_Practice.DataAccess.Infrastructure
{
    public static class DependencyRegister
    {
        public static void RegisterCustomServices(IServiceCollection services)
        {
            // The shared related registry first is intentional
            Shared.Infrastructure.DependencyRegister.RegisterCustomServices(services);

            // Register here your DA related interface pairs into IoC
            services.AddDbContext<MovieDBContext>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
        }
    }
}
