using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Entity;

public class User : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public List<Advertisement>  Advertisements { get; set; }
}