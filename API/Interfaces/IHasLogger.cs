namespace EfCore7.Interfaces;

public interface IHasLogger
{
    public ILogger? Logger { get; set; }
}