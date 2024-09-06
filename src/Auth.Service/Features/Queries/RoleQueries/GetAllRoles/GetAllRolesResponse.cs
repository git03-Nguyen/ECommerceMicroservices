using Auth.Service.Data.Models;

namespace Auth.Service.Features.Queries.RoleQueries.GetAllRoles;

public class GetAllRolesResponse
{
    public GetAllRolesResponse(IEnumerable<ApplicationRole> payload)
    {
        Payload = payload;
    }

    public IEnumerable<ApplicationRole> Payload { get; set; }
}