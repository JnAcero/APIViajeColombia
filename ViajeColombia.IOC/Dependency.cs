using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViajeColombia.BussinesLogic.Clients;
using ViajeColombia.BussinesLogic.Contracts;
using ViajeColombia.BussinesLogic.Utility;
using ViajeColombia.DataAccess.DBContext;
using ViajeColombia.DataAccess.Repositories;
using ViajeColombia.DataAccess.Repositories.Contracts;

namespace ViajeColombia.IOC
{
    public static class Dependency
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ViajeColombiaContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("postgresCnx"));
            });
            services.AddHttpClient("vuelos", client =>
            {
                client.BaseAddress = new Uri("https://bitecingcom.ipage.com/testapi/avanzado.js");
            });
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IApiClientService, ApiClient>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
