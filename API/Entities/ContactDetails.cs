using System.ComponentModel.DataAnnotations;

namespace EfCore7.Entities;

public class ContactDetails
{
    public required Address Address { get; set; }
    
    [MaxLength(999999999)]
    public string? PhoneNumber { get; set; }
}