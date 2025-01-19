using Microsoft.AspNetCore.Identity;

namespace Backend.Models;

public class User : IdentityUser
{
    public ICollection<Review> Reviews { get; set; }
    
}