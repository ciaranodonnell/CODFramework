using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace COD.Platform.Security.AuthenticationService
{
    public class TokenValidationResult
    {
        public bool IsValid { get; internal set; }
        public ClaimsPrincipal ClaimsPrinciple { get; internal set; }
        public SecurityToken ValidatedToken { get; internal set; }
    }
}