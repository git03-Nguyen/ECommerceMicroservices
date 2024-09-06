namespace Order.Service.Extensions;

public static class MediatRServiceExtensions
{
    public static IServiceCollection AddMediatRService(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        return services;
    }
}