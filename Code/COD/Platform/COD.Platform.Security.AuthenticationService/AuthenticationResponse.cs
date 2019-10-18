namespace COD.Platform.Security.AuthenticationService
{
    public class AuthenticationResponse
    {
        public bool Success { get; internal set; }
        public string AuthToken { get; internal set; }
    }
}