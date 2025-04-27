namespace LibraryManagement.Domain.Enums;

public enum BookCopyStatus
{
    Available = 1,
    OnLoan = 2, // Renamed from LoanedOut
    Reserved = 3, // A specific copy reserved for pickup
    Damaged = 4,
    Lost = 5,
    InRepair = 6,
    Withdrawn = 7 // Removed from circulation
}