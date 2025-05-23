using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.DTO.Account;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class AccountController :ControllerBase
{
    private readonly UserManager<User> _userManager;    
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserService _userService;
    
    public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager, IUserService userService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            bool isEmailExist = await _userService.IsEmailTaken(registerDto.Email);
            if (isEmailExist)
                return BadRequest($"Account with email {registerDto.Email} already exists");

            bool isUserNameExist = await _userService.IsUserNameTaken(registerDto.UserName);
            if (isUserNameExist)
                return BadRequest($"Account with username {registerDto.UserName} already exists");
            
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };
            
            var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
            
                if (roleResult.Succeeded)
                {
                    return Ok(
                        new NewUserDto
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.GenerateToken(user)
                        });
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createdUser.Errors);
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        
        if(user == null)
            return Unauthorized("Invalid username or password");
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
        if(!result.Succeeded)
            return Unauthorized("Invalid username or password");

        return Ok(new NewUserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Token = _tokenService.GenerateToken(user)
        });
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        //TODO: Implement logout
        return Ok("Logged out");
    }

    [HttpPost("ChangeUsername")]
    [Authorize]
    public async Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameDto changeUsernameDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized("User not authenticated");
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound("User not found");

        bool isUsernameIsTaken = await _userService.IsUserNameTaken(changeUsernameDto.newUserName);
        if(isUsernameIsTaken)
            return BadRequest("Username already taken");
        
        var result = await _userManager.SetUserNameAsync(user, changeUsernameDto.newUserName);
        if(!result.Succeeded)
            return BadRequest(result.Errors);
        
        return Ok($"Changed username to : {changeUsernameDto.newUserName}");
    }

    [HttpPost("ChangePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized("User not authenticated");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound("User not found");

        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.actualPassword, changePasswordDto.NewPassword);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _signInManager.RefreshSignInAsync(user);
    
        return Ok("Password changed successfully");
    }
    
    [HttpPost("ChangeEmail")]
    [Authorize]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto changeEmailDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized("User not authenticated");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound("User not found");
        
        var result = await _userManager.ChangeEmailAsync(user, changeEmailDto.actualEmail, changeEmailDto.newEmail);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _signInManager.RefreshSignInAsync(user);
    
        return Ok("Email changed successfully");
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] string email)
    {
        //TODO: Implement forgotten password
        return Ok("Email send");
    }
}
