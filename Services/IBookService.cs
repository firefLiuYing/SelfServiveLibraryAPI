using AutoLibraryAPI.Models;

namespace AutoLibraryAPI.Services;

public interface IBookService
{
    Task<List<Book>> GetAvailableBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book> AddBookAsync(Book book);
    Task<bool> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id);
}