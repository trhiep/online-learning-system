using Microsoft.AspNetCore.Identity;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Utils.EmailUtils;

namespace OnlineLearningSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<OLS_DBContext>();
            builder.Services.AddSession();
            builder.Services.AddMemoryCache();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSignalR();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<OLS_DBContext>();
            var app = builder.Build();

            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapHub<SignalRHub>("/SignalRHub");

            app.Run();
        }
    }
}