using FluentValidation;
using MediatR;

namespace Auth.Service.Extensions;

public static class MediatRServiceExtensions
{
    public static IServiceCollection AddMediatRService(this IServiceCollection services)
    {
        #region MediatR and FluentValidation

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        #endregion
        return services;
    }
    
}