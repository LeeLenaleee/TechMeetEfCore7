using EfCore7.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCore7.Services;

public class EmployeeService
{
    private readonly DatabaseContext _dbContext;

    public EmployeeService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddEmployee(Commands.AddEmployee employee, CancellationToken cancellationToken)
    {
        _dbContext.Employees.Add(new()
        {
            Name = employee.Name,
            Age = employee.Age,
            IsOld = employee.IsOld,
            Added = DateTime.UtcNow,
            ContactDetails = new()
            {
                Address = new()
                {
                    Country = "Nederland",
                    City = "Honserlesdijk",
                    Postcode = "1234TA",
                    Street = "Hoge Waard"
                }
            }
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAllOldEmployeesOldWay(CancellationToken cancellationToken)
    {
        _dbContext.RemoveRange(_dbContext.Employees.Where(e => e.IsOld));
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAllOldEmployeesNewWay(CancellationToken cancellationToken)
    {
        await _dbContext.Employees.Where(e => e.IsOld).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task UpdateAllOldEmployeesOldWay(CancellationToken cancellationToken)
    {
        var employees = _dbContext.Employees.Where(e => e.Age >= 25);
        foreach (var employee in employees)
        {
            employee.IsOld = true;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAllOldEmployeesNewWay(CancellationToken cancellationToken)
    {
        await _dbContext.Employees.Where(e => e.Age >= 25).ExecuteUpdateAsync(x => x.SetProperty(e => e.IsOld, e => true), cancellationToken);
    }
}