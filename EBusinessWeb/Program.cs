using EBusinessData.Extensions;
using EBusinessService.Extensions;

namespace EBusinessWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.LoadDataLayerExtension(builder.Configuration);
            builder.Services.LoadServiceLayerExtension();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{Controller=Home}/{Action=Index}/{Id?}"
                );

            app.Run();
        }
    }
}