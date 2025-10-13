using AutoLibraryAPI.Models;
using AutoLibraryAPI.Util;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using LoginRequest = AutoLibraryAPI.Models.LoginRequest;

namespace AutoLibraryAPI.Services;

public interface IUserService
{
    Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    Task<bool> RegisterUserAsync(RegisterRequest registerRequest);
    Task<bool> SuspendUserAsync(int userId, DateTime endTime, string reason);
    Task<bool> ActivateUserAsync(int userId);
}

public class UserService(DataContext context, IJwtService jwtService) : IUserService
{
    private readonly DataContext _context = context;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _context.Users
            .OfType<UserBase>()
            .FirstOrDefaultAsync(u => u.Username == loginRequest.Username && u.IsActive);
        if (user == null||!PasswordHasher.VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new UnauthorizedAccessException("用户名或者密码");
        }
        user.LastLogin = DateTime.Now;
        await _context.SaveChangesAsync();
        var userInfo=MapToUserInfo(user);
        var token = _jwtService.GenerateToken(userInfo);
        return new LoginResponse
        {
            Token = token,
            Expiration = DateTime.Now.AddMinutes(60),
            UserInfo = userInfo,
        };
    }

    public Task<bool> RegisterUserAsync(RegisterRequest registerRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SuspendUserAsync(int userId, DateTime endTime, string reason)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ActivateUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    private UserInfo MapToUserInfo(UserBase user)
    {
        var userInfo = new UserInfo
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            UserType = user.UserType
        };
        
        if (user is AppUser appUser)
        {
            userInfo.FirstName = appUser.FirstName;
            userInfo.LastName = appUser.LastName;
            userInfo.Role = Role.User;
        }
        else if (user is AdminUser adminUser)
        {
            userInfo.Role = adminUser.Role;
        }
        
        return userInfo;
    }
}