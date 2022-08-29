using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProCodeGuide.Samples.BrokenAccessControl.AuthorizationHandlers;
using ProCodeGuide.Samples.BrokenAccessControl.Data;
using ProCodeGuide.Samples.BrokenAccessControl.Repositories;
using ProCodeGuide.Samples.BrokenAccessControl.Services;

namespace ProCodeGuide.Samples.BrokenAccessControl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("IsPostOwnerPolicy", policy =>
                    policy.Requirements.Add(new IsPostOwnerRequirement()));
            });

            builder.Services.AddTransient<IPostsService, PostsService>();
            builder.Services.AddTransient<IPostsRepository, PostsRepository>();
            builder.Services.AddSingleton<IAuthorizationHandler, PostOwnerAuthorizationHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}