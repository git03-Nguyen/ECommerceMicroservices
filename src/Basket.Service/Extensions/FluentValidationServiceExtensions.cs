using Basket.Service.Middlewares;
using FluentValidation;
using MediatR;

namespace Basket.Service.Extensions;

public static class FluentValidationServiceExtensions
{
    public static IServiceCollection AddFluentValidationService(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        return services;
    }
}