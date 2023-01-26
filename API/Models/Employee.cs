namespace EfCore7.Models;

public class Employee
{
    public required string Name { get; init; }
    
    public required int Age { get; init; }
    
    public required bool IsOld { get; init; }
    
    public required DateTime RetrievedFromDatabase { get; init; } 
    
    public DateTime AddedUk { get; init; } 

    public DateTime AddedPacific { get; init; } 
}