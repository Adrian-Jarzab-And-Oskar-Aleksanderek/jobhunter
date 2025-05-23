using System.ComponentModel.DataAnnotations;

namespace Backend.DTO.Account;

public class ChangePasswordDto
{ 
    [Required]
    public string actualPassword { get; set; }
    [Required]
    public string NewPassword { get; set; }
}