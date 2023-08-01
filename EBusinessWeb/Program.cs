using EBusinessData.Extensions;
using EBusinessService.Extensions;
using NToastNotify;

namespace EBusinessWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions
            {
                TimeOut = 5000,
                PositionClass = ToastPositions.TopCenter
            });
            builder.Services.LoadDataLayerExtension(builder.Configuration);
            builder.Services.LoadServiceLayerExtension();
            builder.Services.AddAuthentication().AddFacebook(opt => 
            {
                opt.AppId = "6469197996450199";
                opt.AppSecret = "98c530c24ec8b1d7808ce3f40a5b648d";
            });

            builder.Services.AddAuthentication().AddGoogle(opt =>
            {
                opt.ClientId = "661414414557-4i1h1lve228ppcib6hfolanoo239hc5l.apps.googleusercontent.com";
                opt.ClientSecret = "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";
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNToastNotify();

            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                app.MapControllerRoute(
                name: "default",
                pattern: "{Controller=Home}/{Action=Index}/{Id?}"
                );

            app.Run();
        }
    }
}