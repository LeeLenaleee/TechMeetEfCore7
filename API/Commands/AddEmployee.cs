namespace EfCore7.Commands;

public record AddEmployee
{
    public required string Name { get; init; }
    
    public required int Age { get; init; }
    
    public required bool IsOld { get; init; }

}