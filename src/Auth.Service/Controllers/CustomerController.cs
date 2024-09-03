using Auth.Service.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public CustomerController(ILogger<CustomerController> logger, SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _userManager.Users.ToListAsync();
        return Ok(users);
    }
    
    // Get user by email
    [HttpPost]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null) return NotFound();
        
        return Ok(user);
    }
    
    // Get user by id
    [HttpPost]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        
        if (user == null) return NotFound();
        
        return Ok(user);
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] ApplicationUser user) 
    {
        // TODO
        
        return Ok();
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> ChangePassword(string id, [FromBody] string newPassword)
    {
        var user = await _userManager.FindByIdAsync(id);
        
        if (user == null) return NotFound();
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        
        if (!result.Succeeded) return BadRequest(result.Errors);
        
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null) return NotFound();
        
        var result = await _userManager.DeleteAsync(user);
        
        if (!result.Succeeded) return BadRequest(result.Errors);
        
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        
        if (user == null) return NotFound();
        
        var result = await _userManager.DeleteAsync(user);
        
        if (!result.Succeeded) return BadRequest(result.Errors);
        
        return Ok();
    }
}