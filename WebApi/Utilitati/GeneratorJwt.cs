using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApi.Utilitati;

public static class GeneratorJwt
{
    public static string CreeazaToken(string cheie, string emitent)
    {
        var cheieSecuritate = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cheie));
        var credentiale = new SigningCredentials(cheieSecuritate, SecurityAlgorithms.HmacSha256);
        var tokenSecuritate = new JwtSecurityToken(
            issuer: emitent,
            audience: emitent,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentiale);

        return new JwtSecurityTokenHandler().WriteToken(tokenSecuritate);
    }
}
