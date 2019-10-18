using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace COD.Platform.Security.AuthenticationService
{
    public class AuthenticationService
    {
        private TokenConfiguration tokenConfig;

        public AuthenticationService(TokenConfiguration tokenConfig)
        {
            this.tokenConfig = tokenConfig;
        }
        private string Secret { get; } = Convert.ToBase64String(Encoding.UTF8.GetBytes("ThisIsMySecret"));

        public string GenerateToken(ISecurityProfile user, string secret, string[] roles, int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = tokenConfig.Issuer,
                Audience = tokenConfig.Audience,
                Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, user.SecurityIdentifier)
                }),
                Claims = new Dictionary<string, object> { { "roles", string.Join(",", user.Roles) } },
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }


        public static TokenValidationResult ValidateToken(string token, string secret)
        {


            try
            {
                var symmetricKey = Convert.FromBase64String(secret);
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(symmetricKey);

                var validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = "http://localhost:50191",
                    ValidIssuer = "http://localhost:50191",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = LifetimeValidator,
                    IssuerSigningKey = securityKey
                };
                SecurityToken securityToken;

                //extract and assign the user of the jwt
                var principle = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                return new TokenValidationResult { IsValid = true, ValidatedToken = securityToken, ClaimsPrinciple = principle };

            }
            catch (SecurityTokenValidationException e)
            {
                return new TokenValidationResult { IsValid = false };
                
            }
            catch (Exception ex)
            {
                return new TokenValidationResult { IsValid = false };
            }

            


        }

        public static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            var result = new AuthenticationResponse();
                                  

            //result.AuthToken = token;
            result.Success = true;
            return result;
        }

    }
}
