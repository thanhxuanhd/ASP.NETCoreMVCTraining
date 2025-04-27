namespace LibraryManagement.Domain.Models;

public class Book : IEntityBase<Guid>
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime PublicationDate { get; set; }
    
    public virtual ICollection<Category> Categories { get; set; }
}