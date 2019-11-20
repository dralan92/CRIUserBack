using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        public ApplicationUserController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Route("register")]
        public string Test()
        {
            return "XXXXX";
        }

        [HttpPost]
        [Route("register")]//-->api/AplicationUser/register
        public async Task<Object> PostApplicationUser(ApplicationUserModel model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
                DomainName = model.DomainName,

            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("login")]//-->api/AplicationUser/login
        public async Task<IActionResult> Login( LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var passwordMatch = await _userManager.CheckPasswordAsync(user, model.Password);
            if ( user != null && passwordMatch)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(5),
                    SigningCredentials = new SigningCredentials( 
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)),
                        SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok( new {token});
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect"});
            }
        }

    }
}