using Backend.DTO.Account;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Interfaces;

public interface IUserService
{
    Task<bool> IsEmailTaken(string email);
    Task<bool> IsUserNameTaken(string userName);

}