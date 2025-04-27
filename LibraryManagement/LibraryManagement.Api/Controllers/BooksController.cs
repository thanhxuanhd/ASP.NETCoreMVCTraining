using LibraryManagement.Domain.Enums;
using LibraryManagement.Service.Dto;
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
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string keyword, int? page = 0, int? pageSize = 15)
    {
        var books = await _bookService.Get(keyword, (int)page, (int)pageSize);

        return Ok(books);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = $"{Roles.Member},{Roles.SupperAdmin}")]
    public async Task<ActionResult<BookDto>> GetBook(Guid id)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize(Roles = Roles.SupperAdmin)]
    public async Task<ActionResult<BookDto>> PostBook(CreateBookDto createBookDto)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = Roles.SupperAdmin)]
    public async Task<IActionResult> PutBook(Guid id, UpdateBookDto updateBookDto)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Roles.SupperAdmin)]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}