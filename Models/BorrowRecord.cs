namespace AutoLibraryAPI.Models;

public class BorrowRecord
{
    public int Id { get; set; }
    
    // 外键关系
    public int BookId { get; set; }
    public int AppUserId { get; set; }
    
    // 借阅信息
    public DateTime BorrowDate { get; set; }= DateTime.Now;
    public DateTime DueDate { get; set; } 
    public DateTime ReturnDate { get; set; }
    
    // 状态管理
    public BorrowStatus Status { get; set; } = BorrowStatus.Borrowed;
    
    // 续借
    public int RenewalCount { get; set; } = 0;
    public DateTime? LastRenewalDate { get; set; }
    
    // 罚款相关
    public decimal? FineAmount { get; set; }
    public bool IsFinePaid { get; set; } = false;
    
    // 导航属性
    public Book Book { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
}

public enum BorrowStatus
{
    Requested,
    Approved,
    Borrowed,
    Returned,
    Overdue,
    Lost,
}