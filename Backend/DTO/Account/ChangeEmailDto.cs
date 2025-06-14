using System.ComponentModel.DataAnnotations;

namespace Backend.DTO.Account;

public class ChangeEmailDto
{
    [Required]
    public string newEmail { get; set; }
}