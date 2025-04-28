namespace LibraryManagement.Service.Dtos;

public class BookDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime PublicationDate { get; set; }
}