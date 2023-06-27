using EBusinessData.DAL;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using EBusinessService.Services.Concretes;
using Microsoft.AspNetCore.Identity;
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
            service.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.Password.RequireLowercase = true;
                option.Password.RequireDigit = true;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 3;
                option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.";
                option.User.RequireUniqueEmail = true;
                option.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            return service;
        }
    }
}
