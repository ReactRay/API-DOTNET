using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalksAPI.Models.DTO;

namespace NZwalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager )
        {
            this.userManager = userManager;
        }
        //register method route : api/auth/register

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if(identityResult.Succeeded)
            {
                //add roles to user
                if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("register was successfull");
                    }
                }
            }

            return BadRequest("something went wrong");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.UserName);

            if(user != null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user,loginRequestDTO.Password);

                if (checkPassword)
                {
                    return Ok(" good job !")
                }
            }

            return BadRequest("something was wrong")

        }
    }
}
