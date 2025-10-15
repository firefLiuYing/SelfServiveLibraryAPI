using AutoLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoLibraryAPI.Services;

public interface IUserService
{
    public Task<UserBase?> GetUser(LoginRequest loginRequest);
}

public class UserService(DataContext context) : IUserService
{
    private readonly DataContext _context = context;

    public async Task<UserBase?> GetUser(LoginRequest loginRequest)
    {
        try
        {
            var user = await _context.Users
                .OfType<UserBase>()
                .FirstOrDefaultAsync(u => u.Username == loginRequest.Username);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}