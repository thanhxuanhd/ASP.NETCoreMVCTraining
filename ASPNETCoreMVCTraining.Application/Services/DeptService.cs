using ASPNETCoreMVCTraining.Application.Interfaces;
using ASPNETCoreMVCTraining.Application.ViewModels;
using ASPNETCoreMVCTraining.Domain.Models;
using ASPNETCoreMVCTraining.Persistent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreMVCTraining.Application.Services;

public class DeptService : IDeptService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ILogger<DeptService> _logger;

    public DeptService(ApplicationDbContext applicationDbContext, ILogger<DeptService> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public bool CreateDept(DeptViewModel dept)
    {
        var duplicateDept = _applicationDbContext.Depts.FirstOrDefault(c => c.Name.Equals(dept.Name, StringComparison.OrdinalIgnoreCase));
        if (duplicateDept is not null)
        {
            _logger.LogWarning($"Depart with name {dept.Name} already exists.");
            return false;
        }
        
        _applicationDbContext.Depts.Add(new Dept()
        {
            Name = dept.Name
        });
        _applicationDbContext.SaveChanges();

        return true;
    }

    public DeptCreateEditViewModel GetById(int deptId)
    {
        var dept = _applicationDbContext.Depts.FirstOrDefault(d => d.Id == deptId);

        if (dept is null)
        {
            return new DeptCreateEditViewModel();
        }

        return new DeptCreateEditViewModel()
        {
            Id = dept.Id,
            Name = dept.Name
        };
    }

    public List<DeptViewModel> GetDept()
    {
        return [.. _applicationDbContext.Depts.Select(d => new DeptViewModel()
        {
            Id = d.Id,
            Name = d.Name
        }).AsNoTracking()];
    }

    public bool RemoveDept(int deptId)
    {
        var dept = _applicationDbContext.Depts.Include(d => d.Employees).FirstOrDefault(d => d.Id == deptId);

        if (dept is null)
        {
            _logger.LogError($"Depart with id {deptId} not found.");
            return false;
        }

        if (dept.Employees.Any())
        {
            _logger.LogWarning("Depart has Employees associated with this dept.");
            return false;
        }
        
        _applicationDbContext.Depts.Remove(dept);
        _applicationDbContext.SaveChanges();
        return true;
    }

    public bool UpdateDept(DeptViewModel dept)
    {
       var deptInDatabase = _applicationDbContext.Depts.FirstOrDefault(d => d.Id == dept.Id);

       if (deptInDatabase is null)
       {
           _logger.LogError($"Depart with name {dept.Name} does not exist.");
           return false;
       }
       
       var duplicateDept = _applicationDbContext.Depts.FirstOrDefault(c => c.Name.Equals(dept.Name, StringComparison.OrdinalIgnoreCase));

       if (duplicateDept is not null)
       {
           _logger.LogWarning($"Depart with name {dept.Name} already exists.");
           return false;
       }
       
       deptInDatabase.Name = dept.Name;
       _applicationDbContext.SaveChanges();
       return true;
    }
}