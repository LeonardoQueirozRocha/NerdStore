using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Data;
using NerdStore.Payments.Data;
using NerdStore.Sales.Data;

namespace NerdStore.WebApp.MVC.Configurations;

public static class DbContextConfiguration
{
    public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<SalesContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<PaymentContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}