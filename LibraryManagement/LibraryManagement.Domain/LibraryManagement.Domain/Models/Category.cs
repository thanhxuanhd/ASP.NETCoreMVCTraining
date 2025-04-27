namespace LibraryManagement.Domain.Models;

public class Category : IEntityBase<Guid>
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
}