using System.ComponentModel.DataAnnotations;

namespace Backend.DTO.Account;

public class LoginDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}