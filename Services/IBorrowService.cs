using AutoLibraryAPI.Models;

namespace AutoLibraryAPI.Services;

public interface IBorrowService
{
    Task<BorrowRecord> RequestBorrowAsync(int bookId, int userId);
    Task<bool> ApproveBorrowAsync(int recordId, int adminId);
    Task<bool> ReturnBookAsync(int recordId);
    Task<bool> RenewBookAsync(int recordId);
    Task<List<BorrowRecord>> GetUserBorrowHistoryAsync(int userId);
    Task<List<BorrowRecord>> GetCurrentBorrowsAsync(int userId);
}