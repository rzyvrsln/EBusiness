using EBusinessData.DAL;
using EBusinessData.Repositories;
using EBusinessData.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EBusinessData.Extensions
{
    public static class DataLayerExtension
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("defaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWrok>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
