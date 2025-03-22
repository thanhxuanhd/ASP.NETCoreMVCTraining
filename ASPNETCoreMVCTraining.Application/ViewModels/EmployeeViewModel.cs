namespace ASPNETCoreMVCTraining.Application.ViewModels;

public class EmployeeViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Department { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public int DeptId { get; set; }
    
    public string PhotoPath { get; set; }
}