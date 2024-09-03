using Auth.Service.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AdminController : ControllerBase
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return Ok(roles);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(string roleName)
    {
        var role = new ApplicationRole
        {
            Name = roleName
        };

        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded) return BadRequest(result.Errors);

        return Created("/AuthService/", role);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);

        if (role == null) return NotFound();

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> Assign(string roleName, string email)
    {
        var role = await _roleManager.FindByNameAsync(roleName);

        if (role == null) return NotFound();

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return NotFound();

        var result = await _userManager.AddToRoleAsync(user, role.Name);

        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok();
    }
    
}