using ASPNETCoreMVCTraining.Interfaces;
using ASPNETCoreMVCTraining.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreMVCTraining.Controllers;

public class DeptController : Controller
{
    private readonly ILogger<DeptController> _logger;
    private readonly IDeptService _deptService;

    public DeptController(ILogger<DeptController> logger, IDeptService deptService)
    {
        _logger = logger;
        _deptService = deptService;
    }

    public IActionResult Index()
    {
        var dept = new List<DeptViewModel>();
        try
        {
            dept = _deptService.GetDept();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return View(dept);
    }

    public IActionResult Create()
    {
        return View("CreateEdit", new DeptCreateEditViewModel());
    }

    public IActionResult Edit(int? id)
    {
        if (!id.HasValue)
        {
            _logger.LogError("DeptId is null.");
            return View("CreateEdit", new DeptCreateEditViewModel());
        }
        var model = _deptService.GetById(id.Value);

        return View("CreateEdit", model);
    }
}