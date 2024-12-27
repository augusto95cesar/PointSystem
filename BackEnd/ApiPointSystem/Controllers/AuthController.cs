using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PointSystem.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PointSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(PostLoginDTO login)
        {
            string email = login.Email; 
            string password = login.Password;

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                // Acessar a chave do appsettings.json
                var key = _configuration["JwtSettings:SecretKey"];
                var issuer = _configuration["JwtSettings:Issuer"];
                var audience = _configuration["JwtSettings:Audience"];

                // Configurações do token
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                  new Claim("idUser", $"{user.Id}"),
                  new Claim("Login", $"{user.Email}"),
                  //new Claim(ClaimTypes.Name, request.Username),
                  //new Claim(ClaimTypes.Role, "Admin"),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds
                );

                var tokenWrite = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { token = "Bearer " + tokenWrite });

                //return Ok(new
                //{
                //    token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token),
                //    expiration = token.ValidTo
                //}); 
            }

            return Unauthorized();
        }
    }
}
