using ASPNETCoreMVCTraining.Interfaces;
using ASPNETCoreMVCTraining.Models;
using ASPNETCoreMVCTraining.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreMVCTraining.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public EmployeeService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public bool CreateEmployee(EmployeeViewModel model)
    {
        return true;
    }

    public EmployeeCreateEditViewModel GetById(int employeeId)
    {
        var employee = _applicationDbContext.Employees.FirstOrDefault(e => e.Id == employeeId);

        if (employee is null)
        {
            return new EmployeeCreateEditViewModel();
        }

        return new EmployeeCreateEditViewModel()
        {
            Id = employee.Id,
            DeptId = employee.DeptId,
            DateOfBirth = employee.DateOfBirth,
            Email = employee.Email,
            Name = employee.Name,
            PhotoPath = employee.PhotoPath
        };
    }

    public List<EmployeeViewModel> GetEmployees()
    {
        return [.. _applicationDbContext.Employees.Include(e => e.Department)
        .Select(e => new EmployeeViewModel()
        {
            Id = e.Id,
            Name = e.Name,
            Department = e.Department != null ? e.Department.Name : string.Empty
        }).AsNoTracking()];
    }

    public bool RemoveEmployee(int employeeId)
    {
        return true;
    }

    public bool UpdateEmployee(EmployeeViewModel model)
    {
        return true;
    }
}