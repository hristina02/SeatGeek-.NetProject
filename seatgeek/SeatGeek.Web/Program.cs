using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeatGeek.Data;
 namespace SeatGeek.Web
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SeatGeek.Data.Models;
    using SeatGeek.Data;
    using SeatGeek.Web.Infrastructure.Extensions;
    using SeatGeek.Services.Data.Interfaces;
    using SeatGeek.Web.Infrastructure.ModelBinders;
    using Microsoft.AspNetCore.Mvc;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
    using static System.Runtime.InteropServices.JavaScript.JSType;
    using static Common.GeneralApplicationConstants;
    using SeatGeek.Services.Mapping;
    using SeatGeek.Web.ViewModels.Home;
    using System.Reflection;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<SeatGeekDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {

                options.SignIn.RequireConfirmedAccount =
                    builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireLowercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireUppercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireNonAlphanumeric =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequiredLength =
                    builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");


            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<SeatGeekDbContext>();

            builder.Services.AddApplicationServices(typeof(IEventService));

            builder.Services.AddRecaptchaService();

            builder.Services.AddMemoryCache();

            builder.Services.AddResponseCaching();

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/User/Login";
                cfg.AccessDeniedPath = "/Home/Error/401";

            });

            builder.Services.AddControllersWithViews()
             .AddMvcOptions(options =>
             {
                 options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                 options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
             });

            var app = builder.Build();
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();
            app.EnableOnlineUsersCheck();   

            if (app.Environment.IsDevelopment())
            {
                app.SeedAdministrator(DevelopmentAdminEmail);
            }

            app.UseEndpoints(config =>
            {
                config.MapControllerRoute(
                   name: "areas",
                   pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );

                config.MapControllerRoute(
                    name: "ProtectingUrlRoute",
                    pattern: "/{controller}/{action}/{id}/{information}",
                    defaults: new { Controller = "Category", Action = "Details" });

                config.MapDefaultControllerRoute();

                config.MapRazorPages();
            });

         
         
            app.Run();

        }
    }
}