using Microsoft.EntityFrameworkCore;

namespace AutoLibraryAPI.Models;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<UserBase> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BorrowRecord> BorrowRecords { get; set; }
    
    public DbSet<AppUser>  AppUsers { get; set; }
    public DbSet<AdminUser> AdminUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // 配置TPH继承
        modelBuilder.Entity<UserBase>()
            .HasDiscriminator<string>("UserType")
            .HasValue<AppUser>("AppUser")
            .HasValue<AdminUser>("AdminUser");
        
        // 配置关系
        modelBuilder.Entity<BorrowRecord>()
            .HasOne(b => b.AppUser)
            .WithMany(b => b.BorrowRecords)
            .HasForeignKey(b => b.BookId);
        
        modelBuilder.Entity<BorrowRecord>()
            .HasOne(b => b.AppUser)
            .WithMany(b => b.BorrowRecords)
            .HasForeignKey(b => b.AppUserId);
        
        // 配置索引
        modelBuilder.Entity<Book>()
            .HasIndex(b=>b.ISBN)
            .IsUnique();
        modelBuilder.Entity<UserBase>()
            .HasIndex(u=>u.Username)
            .IsUnique();
        modelBuilder.Entity<UserBase>()
            .HasIndex(u=>u.Email)
            .IsUnique();
        
    }
}