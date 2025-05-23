using Backend.DTO.Account;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Service;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager) 
    {
        _userManager = userManager;
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    public async Task<bool> IsUserNameTaken(string userName)
    {
        return await _userManager.FindByNameAsync(userName) != null;
    }
    

}