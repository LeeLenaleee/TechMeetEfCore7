using EfCore7.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCore7.Repositories;

public class EmployeeRepository
{
    private readonly DatabaseContext _dbContext;

    public EmployeeRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Models.Employee>> GetAllEmployees(CancellationToken cancellationToken)
    {
        var employees = await _dbContext.Employees.AsNoTracking().ToListAsync(cancellationToken);

        return employees.ConvertAll(e => new Models.Employee
        {
            Name = e.Name,
            Age = e.Age,
            IsOld = e.IsOld,
            RetrievedFromDatabase = e.Retrieved,
        });
    }
    
    // public async Task<List<Models.Employee>> GetAllEmployeesDateTime(CancellationToken cancellationToken)
    // {
    //     var employees = await _dbContext
    //         .Employees
    //         .AsNoTracking()
    //         .Select(e => new
    //         {
    //             e.Age,
    //             e.Name,
    //             e.IsOld,
    //             e.Retrieved,
    //             PacificTime = EF.Functions.AtTimeZone(e.Added, "Pacific Standard Time"),
    //             UkTime = EF.Functions.AtTimeZone(post.PublishedOn, "GMT Standard Time"),
    //         })
    //         .ToListAsync(cancellationToken);
    //
    //     return employees.ConvertAll(e => new Models.Employee
    //     {
    //         Name = e.Name,
    //         Age = e.Age,
    //         IsOld = e.IsOld,
    //         RetrievedFromDatabase = e.Retrieved,
    //     });
    // }

    public async Task<List<string>> GetAllEmployeeNames(CancellationToken cancellationToken)
    {
        List<string> names = new();
        var query = _dbContext
            .Employees
            .GroupBy(e => e.Name)
            .AsNoTracking();

        await foreach (var group in query.AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            names.Add(group.Key);
        }

        return names;
    }
    
}