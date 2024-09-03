using Auth.Service.Data.Models;
using Auth.Service.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Features.Queries.UserQueries.GetAllUsers;

public class GetAllUsersHandler: IRequestHandler<GetAllUsersQuery, GetAllUsersResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetAllUsersHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        if (_userManager.SupportsQueryableUsers)
        {
            var users = _userManager.Users;
            var usersDto = await users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                FullName = u.FullName
            }).ToListAsync(cancellationToken);

            foreach (var userDto in usersDto)
            {
                var user = await users.FirstAsync(u => u.Id == userDto.Id, cancellationToken);
                userDto.Roles = await _userManager.GetRolesAsync(user);
            }
            
            return new GetAllUsersResponse(usersDto);
        }
        else
        {
            throw new NotSupportedException("The user manager does not support querying users");
        }
    }
}