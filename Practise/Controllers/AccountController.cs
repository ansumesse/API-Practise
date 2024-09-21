using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Practise.DTO;
using Practise.Models;

namespace Practise.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserApplication> userManager;

        public AccountController(UserManager<UserApplication> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                UserApplication user = new UserApplication()
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                };
                var result = await userManager.CreateAsync(user, registerDTO.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                
            }
            return BadRequest();
        }
    }
}
