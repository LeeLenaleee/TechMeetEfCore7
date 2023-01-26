using System.ComponentModel.DataAnnotations.Schema;
using EfCore7.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EfCore7.Entities;

[Index(nameof(Name), AllDescending = true)]
public class Employee : IHasRetrieved, IHasLogger
{
    private bool _isOld;
    
    public long Id { get; set; }

    public required string Name { get; set; }
    
    public required int Age { get; set; }

    public required bool IsOld
    {
        get => _isOld;
        set
        {
            Console.WriteLine($"Updating IsOld to {value} for {Name}");
            Logger?.LogInformation(1, "Updating IsOld to {Value} for {Name}", value, Name);
            _isOld = value;
        }
    }
    
    public required ContactDetails ContactDetails { get; set; }
    
    public required DateTime Added { get; set; }
    
    [NotMapped]
    public DateTime Retrieved { get; set; }
    
    [NotMapped]
    public ILogger? Logger { get; set; }
}