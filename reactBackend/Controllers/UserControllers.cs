using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using reactBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace reactBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DbContext _context;

        public UserController(DbContext context)
        {
            _context = context;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<dynamic>> Authenticate(User user)
        {
            var authUser = await _context.Set<User>().SingleOrDefaultAsync(u => u.Name == user.Name && u.Password == user.Password);

            if (authUser == null)
            {
                return NotFound(new { message = "Nom d'utilisateur ou mot de passe incorrect." });
            }

            // Génération du jeton JWT
            var token = GenerateJwtToken(authUser);

            return new
            {
                Token = token,
                User = authUser // Renvoie des données en ajouter + si necessaire
            };
        }

        // Méthode pour générer le jeton JWT
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Votre_Clef_Secrète"); // JWT recupéré

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Revendication de l'ID
            new Claim(ClaimTypes.Name, user.Name), // Revendication du nom
                                                       // Ajoutez d'autres revendications selon vos besoins
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Token valable 2 heures
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // Signature du jeton avec la clé secrète
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // Retourne le jeton JWT sous forme de chaîne de caractères
        }

        // endpoints a rajouter si besoin garçon en sucre
    }
}
