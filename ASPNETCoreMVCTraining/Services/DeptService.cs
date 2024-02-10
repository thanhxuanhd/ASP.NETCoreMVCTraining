using ASPNETCoreMVCTraining.Interfaces;
using ASPNETCoreMVCTraining.Models;
using ASPNETCoreMVCTraining.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreMVCTraining.Services;

public class DeptService : IDeptService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public DeptService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public bool CreateDept(DeptViewModel dept)
    {
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
        return true;
    }

    public bool UpdateDept(DeptViewModel dept)
    {
        return true;
    }
}