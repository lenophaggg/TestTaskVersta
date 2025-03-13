using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ordersDbConnection")));

           

            builder.Services.AddScoped<IOrderNumberGenerator, OrderNumberGenerator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

           
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Orders}/{action=List}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
