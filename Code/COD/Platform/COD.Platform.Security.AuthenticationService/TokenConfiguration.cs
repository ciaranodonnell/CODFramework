namespace COD.Platform.Security.AuthenticationService
{
    public class TokenConfiguration
    {
        public string Issuer { get; internal set; }
        public string Audience { get; internal set; }
    }
}