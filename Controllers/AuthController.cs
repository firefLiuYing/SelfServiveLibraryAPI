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
}