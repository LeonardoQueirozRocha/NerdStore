using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Application.AutoMapper;
using NerdStore.Catalog.Data;

namespace NerdStore.WebApp.MVC.Configurations;

public static class WebAppConfiguration
{
    public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllersWithViews();

        services.AddAutoMapper(
            typeof(DomainToViewModelMappingProfile),
            typeof(ViewModelToDomainMappingProfile));

        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    public static void UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityConfiguration();

        app.UseEndpoints(endpoint =>
        {
            endpoint.MapControllerRoute("default", "{controller=Showcase}/{action=Index}/{id?}");
            endpoint.MapRazorPages();
        });
    }
}