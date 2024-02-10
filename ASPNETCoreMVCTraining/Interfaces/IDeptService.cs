using ASPNETCoreMVCTraining.ViewModels;

namespace ASPNETCoreMVCTraining.Interfaces;

public interface IDeptService
{
    List<DeptViewModel> GetDept();

    bool CreateDept(DeptViewModel dept);

    bool UpdateDept(DeptViewModel dept);

    bool RemoveDept(int deptId);

    DeptCreateEditViewModel GetById(int deptId);
}