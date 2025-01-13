
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shopping.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HirePlatform.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;
        public (string Token, int ExpireIn) GenerateToken(User user)
        {
            //Here we now write claims which have id and things after that we will write our premission here
            Claim[] claims = [
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new(JwtRegisteredClaimNames.FamilyName, user.LastName),

                //Recommended for Random GUID
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
            ];
            
            var symmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var singingCredentials =new SigningCredentials(symmetricSecurity, SecurityAlgorithms.HmacSha256);

            //Generate token
            var token = new JwtSecurityToken(
                //Issuer => who make this token
                issuer: _options.Issuer,
                //Audience => who use this token
                audience: _options.Audience,
                //claims => claims
                claims: claims,
                //Expirastion date => datatime now + minutes that you want
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
                //Imp
                signingCredentials: singingCredentials
            );

            return (token: new JwtSecurityTokenHandler().WriteToken(token), ExpiresIn:_options.ExpiryMinutes*60);
        }
    }
}
