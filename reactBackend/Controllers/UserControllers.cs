using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using reactBackend.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace reactBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly User _context;

        public UserController(User context)
        {
            _context = context;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<dynamic>> Authenticate(User user)
        {
            var authUser = await _context.Users.SingleOrDefaultAsync(u => u.Name == user.Name && u.Password == user.Password);

            if (authUser == null)
            {
                return NotFound(new { message = "Nom d'utilisateur ou mot de passe incorrect." });
            }

            var token = GenerateJwtToken(authUser);

            return new
            {
                Token = token,
                User = authUser
            };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Your_Secret_Key");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Durée du token 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor); // Creation du Token
            return tokenHandler.WriteToken(token);
        }
    }
}
