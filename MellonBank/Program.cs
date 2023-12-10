using MellonBank.Data;
using MellonBank.Models;
using MellonBank.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MellonBank.Services;
using MellonBank.Mapper;

namespace MellonBank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("UserContextConnection")
                ?? throw new InvalidOperationException("Connection string 'UserContextConnection' not found.");

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(MapperProfiles));
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<MellonRatesService>();
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{afm?}");

            app.MapRazorPages();

            //Seeding Employee - seeding rates (when the app starts)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var ratesService = new MellonRatesService(context);
                try
                {
                    ratesService.PutRates().Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during application startup: {ex.Message}");
                }

                Seeder seeder = new Seeder();
                seeder.Seed(context, userManager, roleManager).Wait();
            }

            app.Run();
        }
    }
}
