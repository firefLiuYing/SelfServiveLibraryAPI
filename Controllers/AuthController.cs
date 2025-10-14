using AutoLibraryAPI.Models;
using AutoLibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _userService.LoginAsync(loginRequest);
        return Ok(result);
    }

    [HttpGet("test")]
    public async Task<ActionResult<LoginResponse>> Test()
    {
        LoginResponse loginResponse = new LoginResponse
        {
            Expiration = DateTime.Now,
            Token="test",
            UserInfo = new UserInfo()
            {
                Email = "email@email.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Id=1,
                Role = Role.SuperAdmin,
                Username = "username",
                UserType = "AdminUser",
            }
        };
        return Ok(loginResponse);
    }
}