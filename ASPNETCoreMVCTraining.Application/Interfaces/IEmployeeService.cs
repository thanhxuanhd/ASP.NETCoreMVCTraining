using ASPNETCoreMVCTraining.Application.ViewModels;

namespace ASPNETCoreMVCTraining.Application.Interfaces;

public interface IEmployeeService
{
    List<EmployeeViewModel> GetEmployees();

    bool CreateEmployee(EmployeeViewModel model);

    bool UpdateEmployee(EmployeeViewModel model);

    bool RemoveEmployee(int employeeId);

    EmployeeCreateEditViewModel GetById(int employeeId);

    PageData<EmployeeViewModel> GetEmployees(int pageIndex, int pageSize, string employeeName, int? deptId);
}