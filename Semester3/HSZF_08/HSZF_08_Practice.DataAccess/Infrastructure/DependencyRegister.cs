using HSZF_08_Practice.DataAccess.Contexts;
using HSZF_08_Practice.DataAccess.Interfaces;
using HSZF_08_Practice.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HSZF_08_Practice.DataAccess.Infrastructure
{
    public static class DependencyRegister
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Register Shared
            Shared.Infrastructure.DependencyRegister.RegisterServices(services);

            //Register DA
            services.AddDbContext<MovieDBContext>();
            services.AddScoped<IMovieRepository,    MovieRepository>();
            services.AddScoped<IActorRepository,    ActorRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<IRoleRepository,     RoleRepository>();
        }
    }
}
