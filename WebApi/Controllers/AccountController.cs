using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.DTO.AccountDto;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
    private readonly UserManager<AppUser> _usermanager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> usermanager, RoleManager<IdentityRole> roleManager)
        {
            _usermanager = usermanager;
            _roleManager = roleManager;
        }
 
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser user = await _usermanager.FindByNameAsync(registerDto.Username);
            if (user != null) return BadRequest();
            user = new AppUser
            {
                Fullname = registerDto.Username,
                UserName = registerDto.Username
            };
            var result = await _usermanager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            result = await _usermanager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok($"User {user} succesfully registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            AppUser user = await _usermanager.FindByNameAsync(login.Username);
            if(user==null) return StatusCode(404, $"{user} not found");
            if (!await _usermanager.CheckPasswordAsync(user, login.Password))
            {
                return StatusCode(403, "Username or password is wrong");
            }
            List<Claim> claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claim.Add(new Claim("Fullname", user.Fullname));
            claim.Add(new Claim(ClaimTypes.Name, user.UserName));
            var roles = await _usermanager.GetRolesAsync(user);
            claim.AddRange(roles.Select(x=> new Claim(ClaimTypes.Role, x)));
            string secretkey = "0459bb51-2d9e-4124-a571-8b7510e8fdbf";
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretkey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor { 
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddMonths(2),
                SigningCredentials = credentials,
                Audience = "http://localhost:3317/",
                Issuer = "http://localhost:3317/"
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return Ok( new {token = tokenhandler.WriteToken(token)});
        }

   /*     [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            var result = await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            result = await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
            
            return Ok("Roles created");
        }*/
        [Authorize]
        [HttpGet("userprofile")]
        public async Task<IActionResult> GetProfile()
        {
            if (!User.Identity.IsAuthenticated) return StatusCode(401, "Please login");
            
            AppUser user = await _usermanager.FindByNameAsync(User.Identity.Name);
            return Ok(new { name = user.UserName });
        }
    }
}