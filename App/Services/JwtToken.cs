using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.Features.Users.Models;
using Microsoft.IdentityModel.Tokens;

namespace App.Services;

public static class JwtToken
{
    public static string Key { get; private set; } = string.Empty;

    public static void SetJwtKey(string key)
        => Key = key;

    extension(User user)
    {
        public string GenerateToken()
        {
            var handler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(Key);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(4),

                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                ])
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}