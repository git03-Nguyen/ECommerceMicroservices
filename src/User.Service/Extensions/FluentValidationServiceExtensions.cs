using FluentValidation;
using MediatR;
using User.Service.Middlewares;

namespace User.Service.Extensions;

public static class FluentValidationServiceExtensions
{
    public static IServiceCollection AddFluentValidationService(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        return services;
    }
}