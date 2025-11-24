using Character.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Character.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserModel> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

 
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName)) return BadRequest("Role name is empty");

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded) return Ok($"Role {roleName} created");
                return BadRequest(result.Errors);
            }
            return BadRequest("Role already exists");
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound("User not found");

            if (!await _roleManager.RoleExistsAsync(roleName)) return NotFound("Role not found");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded) return Ok($"Role {roleName} assigned to user {email}");

            return BadRequest(result.Errors);
        }
    }
}
