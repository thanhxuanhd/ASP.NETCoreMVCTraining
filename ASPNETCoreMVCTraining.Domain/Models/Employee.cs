using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreMVCTraining.Domain.Models;

public class Employee
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; }
    [Display(Name = "Office Email")]
    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid email format")]
    [Required]
    public string Email { get; set; }

    [Required]
    public int DeptId { get; set; }

    public virtual Dept Department { get; set; }

    public string? PhotoPath { get; set; }

    public DateTime? DateOfBirth { get; set; }
}