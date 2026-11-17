using Microsoft.Extensions.DependencyInjection;

namespace FSync.App.Configuration;

public static class AppConfiguration
{
    public static IServiceCollection AddApp(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<AppDrawer>();

        return serviceCollection;
    }
}
