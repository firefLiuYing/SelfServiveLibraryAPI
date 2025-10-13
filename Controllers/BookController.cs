using AutoLibraryAPI.Models;
using AutoLibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookController(IBookService bookService) : ControllerBase
{
    private readonly IBookService _bookService = bookService;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<Book>>> GetBooks()
    {
        var books = await _bookService.GetAvailableBooksAsync();
        return Ok(books);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Operation")]
    public async Task<ActionResult<Book>> AddBook(Book book)
    {
        var result = await _bookService.AddBookAsync(book);
        return CreatedAtAction(nameof(GetBooks), new { id = result.Id }, result);
    }
    
}