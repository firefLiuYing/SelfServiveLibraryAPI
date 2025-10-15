namespace AutoLibraryAPI.Models;

public abstract class UserBase
{
    public int Id { get; set; }
    public string Username { get; set; }=string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public DateTime CreatedAt { get; set; }=DateTime.Now;
    public DateTime? LastLogin{get; set;}
    public bool IsActive { get; set; }=true;
    // 鉴别器，用于EF Core的TPH（Table Per Hierarchy）继承
    public string UserType { get; set; }=string.Empty;
    public Role Role { get; set; }
}

public class AppUser : UserBase
{
    public bool IsBorrowingSuspended { get; set; } = false;// 是否被禁止借阅
    public DateTime? SuspensionEndDate{get; set;}
    public string? SuspensionReason { get; set; }
    public int MaxBorrowLimit{get; set;}
    // 导航属性
    public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}

public class AdminUser : UserBase
{
    public bool CanManageUsers { get; set; } = true;
    public bool CanManageBooks { get; set; } = true;
    public bool CanManageSystem { get; set; } = true;
}

public enum Role
{
    User,
    Operator,
    Manager,
    SuperAdmin,
}