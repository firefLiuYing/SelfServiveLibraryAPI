namespace AutoLibraryAPI.Models;

public class Book
{
    public int Id { get; set; }
    public string? ISBN { get; set; }
    public string Title { get; set; }=string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? CoverUrl { get; set; }
    
    // 图书状态管理属性
    public int TotalCopies { get; set; } = 1;   // 总本数
    public int AvailableCopies { get; set; } = 1;   // 课被借阅本数
    
    // 导航属性
    public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}