using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeddyCourseYT.Dtos.Account;
using TeddyCourseYT.Interfaces;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountController: ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;
    
    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var appUser = new AppUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                return (roleResult.Succeeded) ? Ok(new NewUserDto
                {
                    Username = appUser.Email,
                    Email = appUser.Email,
                    Token = _tokenService.CreateToken(appUser)
                }) : StatusCode(500, roleResult.Errors);
            }
            else
            {
                return BadRequest(createdUser.Errors);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());
        if (user is null) return Unauthorized("Username is not valid");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized("Username or Password is not correct");

        return Ok(new NewUserDto
        {
            Username = user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        });
    }
}