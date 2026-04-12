public static class KalamServiceCollection
{
    public static IServiceCollection AuthServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}