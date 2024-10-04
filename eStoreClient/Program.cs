using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using eStoreClient.Models;
using eStoreClient.Services;
using BussinessObject;
using Microsoft.EntityFrameworkCore;

namespace eStoreClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();

            // Register the ApiService
            builder.Services.AddScoped(typeof(ApiService<>));
            builder.Services.AddScoped<SalesReportService>();

            // Configure DbContext
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("eStore")));

            builder.Services.Configure<ApiUrls>(builder.Configuration.GetSection("ApiUrls"));

            // Configure Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Configure Authentication using Cookies
            builder.Services.AddAuthentication("Cookies")
                .AddCookie(options =>
                {
                    options.LoginPath = "/Logins/Index"; // Path to login page
                    options.AccessDeniedPath = "/Home/AccessDenied"; // Path for access denied
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Use Session
            app.UseSession();

            // Add routing before authentication/authorization
            app.UseRouting();

            // Use Authentication and Authorization
            app.UseAuthentication(); // Authentication should come before authorization
            app.UseAuthorization();  // Authorization comes after authentication

            // Define routing for controllers
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
