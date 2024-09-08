using Auth.Service.Data.Models;
using Auth.Service.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Features.Queries.UserQueries.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetAllUsersHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        if (!_userManager.SupportsQueryableUsers)
            throw new NotSupportedException("This user manager does not support querying users.");

        var users = _userManager.Users.Where(u => u.IsDeleted == false);
        var usersDto = new List<UserDto>();
        await foreach (var user in users.AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            usersDto.Add(new UserDto(user, role));
        }

        return new GetAllUsersResponse(usersDto);
    }
}