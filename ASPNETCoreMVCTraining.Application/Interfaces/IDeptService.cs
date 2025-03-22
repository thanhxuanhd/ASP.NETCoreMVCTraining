using ASPNETCoreMVCTraining.Application.ViewModels;

namespace ASPNETCoreMVCTraining.Application.Interfaces;

public interface IDeptService
{
    List<DeptViewModel> GetDept();

    bool CreateDept(DeptViewModel dept);

    bool UpdateDept(DeptViewModel dept);

    bool RemoveDept(int deptId);

    DeptCreateEditViewModel GetById(int deptId);
}