using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Auth_Service.Model;
using Auth_Service.DTO;

namespace Auth_Service.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser { UserName = registerDTO.Username };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                if (registerDTO.Role != null && registerDTO.Role.Any())
                {
                    result = await userManager.AddToRolesAsync(identityUser, registerDTO.Roles);

                    if (result.Succeeded)
                    {
                        return Ok("User was registered. Please login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }
    }
}
