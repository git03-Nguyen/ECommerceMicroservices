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
        if (!_userManager.SupportsQueryableUsers) throw new NotSupportedException("This user manager does not support querying users.");
        
        var users = _userManager.Users.Where(u => u.IsDeleted == false);
        var usersDto = await users.Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email
        }).ToListAsync(cancellationToken);

        foreach (var userDto in usersDto)
        {
            var user = await users.FirstAsync(u => u.Id == userDto.Id, cancellationToken);
            userDto.Roles = await _userManager.GetRolesAsync(user);
        }
            
        return new GetAllUsersResponse(usersDto);
    }
}