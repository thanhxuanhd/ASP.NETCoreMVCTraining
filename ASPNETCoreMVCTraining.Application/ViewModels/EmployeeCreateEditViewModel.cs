using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPNETCoreMVCTraining.Application.ViewModels;

public class EmployeeCreateEditViewModel
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Office Email")]
    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required]
    public int DeptId { get; set; }

    public string PhotoPath { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public List<SelectListItem> Departments { get; set; } = new List<SelectListItem>();
}