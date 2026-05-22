public static class KalamServiceCollection
{
    public static IServiceCollection AuthServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }

    public static IServiceCollection UserServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        return services;
    }
}