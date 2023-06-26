using EBusinessService.Services.Abstraction;
using EBusinessService.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace EBusinessService.Extensions
{
    public static class ServiceLayerExtension
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection service)
        {
            service.AddScoped<IPositionService, PositionService>();
            service.AddScoped<IEmployeeService, EmployeeService>();
            service.AddScoped<IContactService, ContactService>();
            return service;
        }
    }
}
