using LibraryManagement.Persistent;
using LibraryManagement.Service.Dto;
using LibraryManagement.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Service.Services;

public class BookService(ApplicationDbContext dbContext) : IBookService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Guid?> CreateBook(CreateBookDto dto)
    {
        var model = dto.ToDomainModel();

        _dbContext.Books.Add(model);
        await _dbContext.SaveChangesAsync();

        return model.Id;
    }

    public async Task<bool> UpdateBook(UpdateBookDto dto)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == dto.Id);

        if (book is null)
        {
            return false;
        }

        dto.MapDtoToDomainModel(book);
        _dbContext.Books.Update(book);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<bool> DeleteBook(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BookDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookDto>> Get(string keyword = "", int pageSize = 15, int pageIndex = 0)
    {
        throw new NotImplementedException();
    }
}