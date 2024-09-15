using Contracts.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace Contracts.Extensions;

public static class CustomAuthorizationExtensions
{
    public static IServiceCollection AddCustomAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(CustomPolicyNameConstants.AdminOnly,
                policy =>
                {
                    policy.RequireAssertion(context => context.User.HasClaim(ClaimConstants.RoleClaimType, ApplicationRoleConstants.Admin) ||
                                                       context.User.HasClaim(ClaimConstants.ClientIdClaimType, ClaimValueConstants.CredentialClient));
                });

            options.AddPolicy(CustomPolicyNameConstants.SellerOnly,
                policy =>
                {
                    policy.RequireAssertion(context => context.User.HasClaim(ClaimConstants.RoleClaimType, ApplicationRoleConstants.Seller) ||
                                                       context.User.HasClaim(ClaimConstants.ClientIdClaimType, ClaimValueConstants.CredentialClient));
                });
            
            options.AddPolicy(CustomPolicyNameConstants.CustomerOnly,
                policy =>
                {
                    policy.RequireAssertion(context => context.User.HasClaim(ClaimConstants.RoleClaimType, ApplicationRoleConstants.Customer) ||
                                                       context.User.HasClaim(ClaimConstants.ClientIdClaimType, ClaimValueConstants.CredentialClient));
                });
            
            options.AddPolicy(CustomPolicyNameConstants.CustomerOrSeller,
                policy =>
                {
                    policy.RequireAssertion(context => context.User.HasClaim(ClaimConstants.RoleClaimType, ApplicationRoleConstants.Seller) ||
                                                       context.User.HasClaim(ClaimConstants.RoleClaimType, ApplicationRoleConstants.Customer) ||
                                                       context.User.HasClaim(ClaimConstants.ClientIdClaimType, ClaimValueConstants.CredentialClient));
                });
        });

        return services;
    }
}