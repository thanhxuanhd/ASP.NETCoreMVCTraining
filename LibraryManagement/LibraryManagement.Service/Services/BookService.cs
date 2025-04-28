using LibraryManagement.Persistent;
using LibraryManagement.Service.Dtos;
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

    public async Task<BookDto?> GetById(Guid id)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

        if (book is null)
        {
            return null;
        }

        return new BookDto()
        {
            Id = book.Id,
            Description = book.Description ?? string.Empty,
            Title = book.Title,
            PublicationDate = book.PublicationDate
        };
    }

    public async Task<IEnumerable<BookDto>> Get(string? keyword = "", int? pageSize = 15, int? pageIndex = 0)
    {
        var query = _dbContext.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(b =>
                b.Title.Contains(keyword) ||
                (!string.IsNullOrWhiteSpace(b.Description) && b.Description.Contains(keyword)));
        }

        pageSize ??= 15;
        pageIndex ??= 0;
        query = query.OrderBy(b => b.Title).Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);

        return await query.AsNoTracking().Select(b => new BookDto()
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description ?? string.Empty
        }).ToListAsync();
    }
}