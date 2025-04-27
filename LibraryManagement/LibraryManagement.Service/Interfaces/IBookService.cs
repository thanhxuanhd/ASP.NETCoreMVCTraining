using LibraryManagement.Service.Dto;

namespace LibraryManagement.Service.Interfaces;

public interface IBookService
{
    Task<Guid?> CreateBook(CreateBookDto dto);
    
    Task<bool> UpdateBook(UpdateBookDto dto);

    Task<bool> DeleteBook(Guid id);

    Task<BookDto> GetById(Guid id);
    
    Task<IEnumerable<BookDto>> Get(string keyword = "", int pageSize = 15, int pageIndex = 0);
}