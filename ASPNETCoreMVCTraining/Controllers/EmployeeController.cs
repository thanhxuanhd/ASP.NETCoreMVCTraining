using ASPNETCoreMVCTraining.Interfaces;
using ASPNETCoreMVCTraining.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPNETCoreMVCTraining.Controllers;

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

    public IActionResult Index(int pageIndex = 1, int pageSize = 20)
    {
        var pageData = new PageData<EmployeeViewModel>();
        try
        {
            pageData = _employeeService.GetEmployees(pageIndex, pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        pageData.PageInfo.Controller = "Employee";
        pageData.PageInfo.Action = "Index";

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