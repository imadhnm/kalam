public static class KalamServiceCollection
{
    public static IServiceCollection UserServices(this IServiceCollection services)
    {
        services.AddScoped<IKalamUserService, KalamUserService>();
        return services;
    }
}