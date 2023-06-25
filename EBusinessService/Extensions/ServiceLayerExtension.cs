using Microsoft.Extensions.DependencyInjection;

namespace EBusinessService.Extensions
{
    public static class ServiceLayerExtension
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection service)
        {
            //service.AddScoped<ICategoryService, CategoryService>();
            return service;
        }
    }
}
