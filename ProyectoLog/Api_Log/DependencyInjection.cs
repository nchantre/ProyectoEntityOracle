using DataAccess.Models;
using Infraestructura.Contratos;
using Infraestructura.Servicios;
using Microsoft.EntityFrameworkCore;

namespace Api_Log
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
           // services.AddScoped<IService, Service>();
            services.AddScoped<IRepositoryAsync<Categoria>, RepositoryAsync<Categoria>>();
        

            //services.AddDbContext<PvContext>(opt =>
            //{
            //    opt.UseSqlServer(configuration.GetConnectionString("PvConnection"));

            //});

            var cadenaConexion = configuration.GetConnectionString("defaultConnection");

            services.AddDbContext<ModelContext>(x =>
              x.UseOracle(
                  cadenaConexion,
                  options => options.UseOracleSQLCompatibility("11"))
              );

            return services;
        }
    }
}
