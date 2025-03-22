using ASPNETCoreMVCTraining.Application.Interfaces;
using ASPNETCoreMVCTraining.Application.ViewModels;
using ASPNETCoreMVCTraining.Domain.Models;
using ASPNETCoreMVCTraining.Persistent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreMVCTraining.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(ApplicationDbContext applicationDbContext, ILogger<EmployeeService> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public bool CreateEmployee(EmployeeViewModel model)
    {
        var employee = new Employee()
        {
            PhotoPath = model.PhotoPath,
            Email = model.Email,
            Name = model.Name,
            DeptId = model.DeptId
        };
        
        _applicationDbContext.Employees.Add(employee);
        _applicationDbContext.SaveChanges();
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

    public PageData<EmployeeViewModel> GetEmployees(int pageIndex, int pageSize, string employeeName, int? deptId)
    {
        var queries = _applicationDbContext.Employees.Include(e => e.Department).AsQueryable();

        if (!string.IsNullOrEmpty(employeeName))
        {
            queries = queries.Where(e => e.Name.Contains(employeeName));
        }

        if (deptId.HasValue)
        {
            queries = queries.Where(e => e.DeptId == deptId);
        }

        var totalCount = queries.Count();

        var employees = queries
            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
            .Select(e => new EmployeeViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Department = e.Department != null ? e.Department.Name : string.Empty,
                Email = e.Email
            }).AsNoTracking();

        return new PageData<EmployeeViewModel>()
        {
            Data = employees,
            PageInfo = new PageInfo()
            {
                PageIndex = pageIndex,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                TotalRecords = totalCount,
                PageSize = pageSize
            }
        };
    }

    public bool RemoveEmployee(int employeeId)
    {
        var emplopee = _applicationDbContext.Employees.FirstOrDefault(x => x.Id == employeeId);

        if (emplopee is null)
        {
            _logger.LogError("Employee not found");
            return false;
        }
        
        _applicationDbContext.Employees.Remove(emplopee);
        _applicationDbContext.SaveChanges();
        return false;
    }

    public bool UpdateEmployee(EmployeeViewModel model)
    {
        var emplopee = _applicationDbContext.Employees.FirstOrDefault(x => x.Id == model.Id);

        if (emplopee is null)
        {
            _logger.LogError("Employee not found");
            return false;
        }
        
        emplopee.Name = model.Name;
        emplopee.Email = model.Email;
        emplopee.DateOfBirth = model.DateOfBirth;
        emplopee.DeptId = model.DeptId;
        
        _applicationDbContext.SaveChanges();
        return false;
    }
}