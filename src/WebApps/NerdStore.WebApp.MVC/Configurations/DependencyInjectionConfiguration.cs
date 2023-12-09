using NerdStore.Catalog.Application.Configuration;
using NerdStore.Core.Configurations;
using NerdStore.Payments.Data.Configurations;
using NerdStore.Sales.Application.Configurations;

namespace NerdStore.WebApp.MVC.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddCoreDependencies();
        services.AddCatalogDependencies();
        services.AddSalesDependencies();
        services.AddPaymentDependencies();
    }
}