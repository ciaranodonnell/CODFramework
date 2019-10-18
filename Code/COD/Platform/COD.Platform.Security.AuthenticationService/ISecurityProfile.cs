using System.Collections.Generic;

namespace COD.Platform.Security.AuthenticationService
{
    public interface ISecurityProfile
    {
                
        string SecurityIdentifier { get; set; }
        
        IEnumerable<string> Roles { get; set; }

    }
}