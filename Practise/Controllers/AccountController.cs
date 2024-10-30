using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Practise.DTO;
using Practise.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practise.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserApplication> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<UserApplication> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
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
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginDTO.UserName);
                if(user is not null)
                {
                    bool isAuthorized = await userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (isAuthorized)
                    {
                        var authClaims = new List<Claim>();
                        authClaims.Add(new Claim(ClaimTypes.Name, user.UserName!));
                        authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

                        var token = new JwtSecurityToken(
                            expires: DateTime.Now.AddHours(5),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
                return Unauthorized();
            }
            return BadRequest();
        }
    }
}
