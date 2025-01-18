using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

using System.ComponentModel.DataAnnotations;

public class User : IdentityUser
{
    public ICollection<Review> Reviews { get; set; }
}