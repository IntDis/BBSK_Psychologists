using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Models;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [HttpPost]
        public string Login([FromBody] LoginRequest request)
        {
            if (request == default || request.Email == default) return string.Empty;

            Role role;

            if(request.Email == "manager@p.ru")
            {
                role = Role.Manager;
            }
            else
            {
                if(request.Email == "psyh@p.ru")
                {
                    role = Role.Psychologist;
                }
                else
                {
                    role = Role.Client;
                }
            }
            var tmp = role.ToString();

            var roleClaim = new Claim(ClaimTypes.Role, role.ToString()); //присвоение ролей (клиент, психолог, менеджер)

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Email), new Claim(ClaimTypes.NameIdentifier, request.ToString()), roleClaim };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)), // время действия - 1 сутки
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
