namespace Test.Extensions;

public static class ServiceRegistrations
{
    public static IServiceCollection RegisterRequiredDependencies(this IServiceCollection services)
    {
        services.AddMappers()
                .AddRepositories()
                .AddControllers();
        
        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
}