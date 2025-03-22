using ASPNETCoreMVCTraining.Application.Interfaces;
using ASPNETCoreMVCTraining.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreMVCTraining.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IEmployeeService _employeeService;

    public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
    {
        _logger = logger;
        _employeeService = employeeService;
    }

    [HttpGet("GetAll", Name = "GetAll")]
    public ActionResult<List<EmployeeViewModel>> GetAll()
    {
        List<EmployeeViewModel> employees = [];
        try
        {
            employees = _employeeService.GetEmployees();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return Ok(employees);
    }

    [HttpGet]
    public ActionResult<PageData<EmployeeViewModel>> GetEmployee(int? deptId, string? name, int pageIndex = 0,
        int pageSize = 15)
    {
        var pageData = new PageData<EmployeeViewModel>();

        try
        {
           pageData = _employeeService.GetEmployees(pageIndex, pageSize, name, deptId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }

        return Ok(pageData);
    }

    [HttpGet("{id}")]
    public ActionResult<EmployeeCreateEditViewModel> GetEmployeeById(int? id)
    {
        try
        {
            var employee = _employeeService.GetById(id.Value);
            return Ok(employee);
        }
        catch (Exception ex)
        {
           _logger.LogError(ex.Message);
           return BadRequest();
        }
    }
    
    [HttpPost]
    public ActionResult Create([FromBody] EmployeeViewModel employee)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(c => c.Value));
            }

            var isSuccess = _employeeService.CreateEmployee(employee);
            if (isSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int? id, [FromBody] EmployeeViewModel employee)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(c => c.Value));
            }

            if (id is null)
            {
                return BadRequest();
            }

            var isSuccess = _employeeService.UpdateEmployee(employee);
            if (isSuccess)
            {
                return NoContent();
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }   
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int? id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var isSuccess = _employeeService.RemoveEmployee(id.Value);
            if (isSuccess)
            {
                return NoContent();
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
}