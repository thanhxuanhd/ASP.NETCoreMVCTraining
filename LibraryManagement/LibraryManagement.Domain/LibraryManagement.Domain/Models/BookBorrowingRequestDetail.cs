namespace LibraryManagement.Domain.Models;

public class BookBorrowingRequestDetail : IEntityBase<Guid>
{
    public Guid Id { get; set; }
    
    public DateTime? ExpiryDate { get; set; } 
    
    public Guid BookId { get; set; } 
    
    public virtual Book Book { get; set; }
}