using EfCore7.Repositories;
using EfCore7.Services;
using Microsoft.AspNetCore.Mvc;

namespace EfCore7.Controllers;

[Route("employee")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeRepository _employeeRepository;
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeRepository employeeRepository, EmployeeService employeeService)
    {
        _employeeRepository = employeeRepository;
        _employeeService = employeeService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Models.Employee>>> GetAllEmployees(CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetAllEmployees(cancellationToken);
    }

    [HttpGet("names")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<string>>> GetAllEmployeeNames(CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetAllEmployeeNames(cancellationToken);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddEmployee(Commands.AddEmployee addEmployee, CancellationToken cancellationToken)
    {
        await _employeeService.AddEmployee(addEmployee, cancellationToken);
        return Ok();
    }

    [HttpDelete("old-delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteAllEmployeesOldWay(CancellationToken cancellationToken)
    {
        await _employeeService.DeleteAllOldEmployeesOldWay(cancellationToken);
        return Ok();
    }
    
    [HttpDelete("new-delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteAllEmployeesNewWay(CancellationToken cancellationToken)
    {
        await _employeeService.DeleteAllOldEmployeesNewWay(cancellationToken);
        return Ok();
    }
    
    [HttpPut("old-update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateAllEmployeesOldWay(CancellationToken cancellationToken)
    {
        await _employeeService.UpdateAllOldEmployeesOldWay(cancellationToken);
        return Ok();
    }
    
    [HttpPut("new-update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateAllEmployeesNewWay(CancellationToken cancellationToken)
    {
        await _employeeService.UpdateAllOldEmployeesNewWay(cancellationToken);
        return Ok();
    }
}