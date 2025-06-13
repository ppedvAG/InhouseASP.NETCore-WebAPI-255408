using BusinessLogic.Contracts;
using BusinessLogic.Data;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RecipeDbApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            ILogger<AccountController> logger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new AppUser
                {
                    UserName = registerDto.UserName,
                    NormalizedUserName = registerDto.UserName?.ToUpper(),
                    Email = registerDto.Email,
                    NormalizedEmail = registerDto.Email?.ToUpper()
                };
                var userResult = await _userManager.CreateAsync(user, registerDto.Password);

                if (userResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, Seed.USER_ROLE);
                    if (roleResult.Succeeded)
                    {
                        return Ok(new 
                        {
                            user.Email,
                            user.UserName,
                            Token = _tokenService.CreateToken(user)
                        });
                    }
                    return BadRequest(roleResult.Errors);
                }
                return BadRequest(userResult.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password,
                    isPersistent: false, lockoutOnFailure: false); 
                if (result.Succeeded)
                {
                    return Ok(new 
                    {
                        user.Email,
                        user.UserName,
                        Token = _tokenService.CreateToken(user)
                    });
                }
            }
            return Unauthorized("Invalid username or password");
        }

    }
}
