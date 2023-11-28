using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaJuridicoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;
        private string _secret;
        public LoginController(IConfiguration configuration, SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _secret = configuration.GetSection("SecretKey").Value;
        }
        public string GenerateToken(USUARIO usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", usuario.ID_USUARIO.ToString()),
                    new Claim("nome", usuario.NOME_USUARIO!),
                    new Claim("tipo", usuario.ACESSO_GESTAO.ToString()),
                    
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("auth")]
        public async Task<ActionResult<dynamic>> AuthAsync([FromBody] USUARIO dto)
        {
            var usuarionDTO = await _sistemaJuridicoDbContext.USUARIO.FirstOrDefaultAsync(x => x.NOME_USUARIO.Equals(dto.NOME_USUARIO) && x.SENHA.Equals(dto.SENHA));

            if (usuarionDTO == null)
                return NotFound();

            string token = this.GenerateToken(usuarionDTO);
            return Ok(new { token = token });
        }
    }
}
