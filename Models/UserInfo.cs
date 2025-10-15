namespace AutoLibraryAPI.Models;

public class UserInfo
{
    public int Id { get; set; }
    public string Username { get; set; }=string.Empty;
    public string Email { get; set; }=string.Empty;
    public string UserType { get; set; }=string.Empty;
    
    public Role Role { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public UserInfo() { }

    public UserInfo(UserBase user)
    {
        Id = user.Id;
        Username = user.Username;
        Email = user.Email;
        UserType = user.UserType;
        Role = user.Role;
        FirstName = user.FirstName;
        LastName = user.LastName;
    }
}