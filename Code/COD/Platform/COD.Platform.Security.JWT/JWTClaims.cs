using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Security.JWT
{
    public class JWTClaims
    {

        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public string ExpirationTime { get; set; }
        public string NotBefore { get; set; }
        public string IssuedAt{ get; set; }
        public string JWTID{ get; set; }
        public Dictionary<string, string> OtherClaims { get; set; }
        

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            if (Issuer != null) sb.Append("iss:\"").Append(Issuer).Append("\",");
            if (Subject != null) sb.Append("sub:\"").Append(Subject).Append("\",");
            if (Audience != null) sb.Append("aud:\"").Append(Audience).Append("\",");
            if (ExpirationTime != null) sb.Append("exp:\"").Append(ExpirationTime).Append("\",");
            if (NotBefore != null) sb.Append("nbf:\"").Append(NotBefore).Append("\",");
            if (IssuedAt != null) sb.Append("iat:\"").Append(IssuedAt).Append("\",");
            if (JWTID != null) sb.Append("jti:\"").Append(JWTID).Append("\",");


            foreach (var claim in OtherClaims) sb.Append(claim.Key).Append(":\"").Append(claim.Value).Append("\",");

            return sb.ToString();
        }


    }
}