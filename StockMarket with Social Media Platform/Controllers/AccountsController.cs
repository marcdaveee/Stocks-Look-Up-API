using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket_with_Social_Media_Platform.Dtos.Account;
using StockMarket_with_Social_Media_Platform.Interfaces;
using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Use Try Catch Block to handle errors gracefully
            try
            {
                //Validate model (logindto) that was passed from Body
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Find if User Exists
                var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == loginDto.UserName.ToLower());

                //Reject Login if Username not found
                if (user == null)
                    return Unauthorized("Invalid username!");

                //use the injected signInManager object to validate the login credentials
                var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                //Check if login was successful
                if (!loginResult.Succeeded)
                    return Unauthorized("Username not found and/or password is incorrect");

                // Return username, email and the generated token upon successful login
                return Ok(new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            


        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        });
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
