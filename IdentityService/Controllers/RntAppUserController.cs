using IdentityService.Data;
using IdentityService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RntAppUserController : ControllerBase
    {
        private UserManager<RntAppUser> _userManager;
        private SignInManager<RntAppUser> _signInManager;
        private readonly ApplicationSettings _appSettings;

        public RntAppUserController(UserManager<RntAppUser> userManager,
                                    SignInManager<RntAppUser> signInManager,
                                    IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/RntAppUser/Register
        public async Task<Object> PostAppUser(UserDetail userDetail)
        {
            var newAppUser = new RntAppUser
            {
                UserName = userDetail.UserName,
                Email = userDetail.Email,
                FullName = userDetail.FullName
            };

            try
            {
                var result = await _userManager.CreateAsync(newAppUser, userDetail.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }


        [HttpPost]
        [Route(nameof(LogIn))]
        //POST : /api/RntAppUser/LogIn
        public async Task<IActionResult> LogIn(LogInDTO logInDTO)
        {
            var user = await _userManager.FindByNameAsync(logInDTO.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, logInDTO.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(
                                         new SymmetricSecurityKey(
                                             Encoding.UTF8.GetBytes(_appSettings.jwtSecure)),
                                             SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "User name or password is incorrect." });
            }

        }

        [HttpGet]
        [Authorize]
        [Route(nameof(GetUserProfile))]
        //GET : /api/RntAppUser/GetUserProfile
        public async Task<Object> GetUserProfile()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }


        // GET api/values
        [HttpGet]
        public static ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Rnt value1", "Rnt value2" };
        }
    }
}