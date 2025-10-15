using AutoLibraryAPI.Models;
using AutoLibraryAPI.Services;
using AutoLibraryAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace AutoLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    public async Task<ActionResult<UserInfo>> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _userService.GetUser(loginRequest);
        if (user == null) return Unauthorized("用户不存在");
        if (!PasswordHasher.VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
            return Unauthorized("账号或密码错误");
        if (!user.IsActive) return Unauthorized("账号被封禁中");
        UserInfo userInfo = new(user);
        return Ok(userInfo);
    }

    [HttpGet("test")]
    public async Task<ActionResult<UserInfo>> Test()
    {
        var userInfo = new UserInfo()
        {
            Email = "email@email.com",
            FirstName = "FirstName",
            LastName = "LastName",
            Id = 1,
            Role = Role.SuperAdmin,
            Username = "username",
            UserType = "AdminUser",
        };
        return Ok(userInfo);
    }
}