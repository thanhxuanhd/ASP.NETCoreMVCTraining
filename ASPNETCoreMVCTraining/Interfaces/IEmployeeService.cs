using ASPNETCoreMVCTraining.Models;
using ASPNETCoreMVCTraining.ViewModels;

namespace ASPNETCoreMVCTraining.Interfaces;

public interface IEmployeeService
{
    List<EmployeeViewModel> GetEmployees();

    bool CreateEmployee(EmployeeViewModel model);

    bool UpdateEmployee(EmployeeViewModel model);

    bool RemoveEmployee(int employeeId);

    EmployeeCreateEditViewModel GetById(int employeeId);

    PageData<EmployeeViewModel> GetEmployees(int pageIndex, int pageSize);
}