using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Models;

public class BookBorrowingRequest : IEntityBase<Guid>
{
    public Guid Id { get; set; }

    public Guid RequestorId { get; set; }
    
    public virtual User Requestor { get; set; }
    
    public Guid? ApproverId { get; set; }
    
    public virtual User? Approver { get; set; }
    
    public DateTime RequestDate { get; set; }
    
    public RequestStatus Status { get; set; }
    
    public virtual ICollection<BookBorrowingRequestDetail> BookBorrowingRequestDetails { get; set; }
}