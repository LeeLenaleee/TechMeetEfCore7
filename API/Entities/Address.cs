namespace EfCore7.Entities;

public class Address
{
    public required string Street { get; set; }
    
    public required string City { get; set; }
    
    public required string Postcode { get; set; }
    
    public required string Country { get; set; }
}