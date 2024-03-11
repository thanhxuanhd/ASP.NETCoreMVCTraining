using ASPNETCoreMVCTraining.Interfaces;
using ASPNETCoreMVCTraining.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPNETCoreMVCTraining.Controllers;

[Authorize]
public class EmployeeController : Controller
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IEmployeeService _employeeService;
    private readonly IDeptService _deptService;

    public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, IDeptService deptService)
    {
        _logger = logger;
        _employeeService = employeeService;
        _deptService = deptService;
    }

    public IActionResult Index(string employeeName, int? deptId, int pageIndex = 1, int pageSize = 20)
    {
        var pageData = new PageData<EmployeeViewModel>();
        try
        {
            pageData = _employeeService.GetEmployees(pageIndex, pageSize, employeeName, deptId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        pageData.PageInfo.Controller = "Employee";
        pageData.PageInfo.Action = "Index";
        ViewData["EmployeeName"] = employeeName;
        ViewData["DeptId"] = deptId;
        pageData.PageInfo.Params = new Dictionary<string, string>
        {
            { "employeeName", employeeName },
            { "deptId", deptId.ToString() }
        };

        return View(pageData);
    }

    public IActionResult Create()
    {
        var model = new EmployeeCreateEditViewModel
        {
            Departments = GetDept()
        };
        return View("CreateEdit", model);
    }

    public IActionResult Edit(int? id)
    {
        if (!id.HasValue)
        {
            _logger.LogError("EmployeeId is null.");
            return View("CreateEdit", new EmployeeCreateEditViewModel());
        }

        var model = _employeeService.GetById(id.Value);
        model.Departments = GetDept();

        return View("CreateEdit", model);
    }

    private List<SelectListItem> GetDept()
    {
        var depts = new List<SelectListItem>()
        {
            new SelectListItem(){
                Value = "",
                Text= "Please select Dept"
            }
        };

        depts.AddRange(_deptService.GetDept().Select(d => new SelectListItem()
        {
            Value = d.Id.ToString(),
            Text = d.Name
        }).ToList());

        return depts;
    }
}