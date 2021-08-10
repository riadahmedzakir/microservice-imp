using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using identity.API.Entities;
using identity.API.Data.Token;

namespace identity.API.Repositories
{
    public class JWTService: IJWTService
    {
        public string GenerateToken(string userName, string Password)
        {
            List<Claim> permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("UserName", "Riad Ahmed Zakir"));
            permClaims.Add(new Claim("userid", "sdasdsadasdsa1"));
            permClaims.Add(new Claim("DFSFSD", "bilal"));

            JWTContainer jwtConfiguration = new JWTContainer { 
                Claims = permClaims
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(null,
                            null,
                            permClaims,
                            expires: DateTime.Now.AddMinutes(jwtConfiguration.ExpireMinutes),
                            signingCredentials: credentials);
            string jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }

        public bool IsTokenValid(string token)
        {
            JWTContainer jwtConfiguration = new JWTContainer();
            //SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey));
            //SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = null,
                ValidAudience = null,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey))
            };
            SecurityToken validatedToken = null;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            }
            catch (SecurityTokenException ex)
            {
                var x = ex;
                return false;
            }
            return true;
        }
    }
}