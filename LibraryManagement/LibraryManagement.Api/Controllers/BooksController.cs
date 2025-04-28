using LibraryManagement.Domain.Enums;
using LibraryManagement.Service.Dtos;
using LibraryManagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class BooksController(IBookService bookService) : ControllerBase
{
    private IBookService _bookService = bookService;

    [HttpGet]
    [Authorize(Roles = $"{Roles.Member},{Roles.SupperAdmin}")]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string? keyword = "", int? page = 0, int? pageSize = 15)
    {
        var books = await _bookService.Get(keyword, pageSize, page);

        return Ok(books);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = $"{Roles.Member},{Roles.SupperAdmin}")]
    public async Task<ActionResult<BookDto>> GetBook(Guid id)
    {
        var book = await _bookService.GetById(id);

        return book is null ? NotFound() : Ok(book) ;
    }

    [HttpPost]
    [Authorize(Roles = Roles.SupperAdmin)]
    public async Task<ActionResult<bool>> PostBook(CreateBookDto createBookDto)
    {
        var bookId = await _bookService.CreateBook(createBookDto);
        return Ok(bookId);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = Roles.SupperAdmin)]
    public async Task<IActionResult> PutBook(Guid id, UpdateBookDto updateBookDto)
    {
        var success = await _bookService.UpdateBook(updateBookDto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Roles.SupperAdmin)]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var success = await _bookService.DeleteBook(id);
        return success ? NoContent() : BadRequest();
    }
}