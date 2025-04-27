using LibraryManagement.Domain.Models;

namespace LibraryManagement.Service.Dto;

public class UpdateBookDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime PublicationDate { get; set; }

    public void MapDtoToDomainModel(Book book)
    {
        book.Description = Description;
        book.Title = Title;
        book.PublicationDate = PublicationDate;
    }
}