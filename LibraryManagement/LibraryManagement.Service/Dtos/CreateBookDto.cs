using System.ComponentModel.DataAnnotations;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Service.Dtos;

public class CreateBookDto
{
    [Required]
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime PublicationDate { get; set; }

    public Book ToDomainModel()
    {
        return new Book()
        {
            Description = Description,
            Id = Guid.NewGuid(),
            PublicationDate = PublicationDate,
            Title = Title
        };
    }
}