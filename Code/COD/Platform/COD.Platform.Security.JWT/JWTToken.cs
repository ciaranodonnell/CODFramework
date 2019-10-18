using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace COD.Platform.Security.JWT
{
    public class JWTToken
    {
        public string this[string key]
        {
            get
            {
                return this.claimsDictionary[key];
            }
        }

        public string Signature { get; private set; }


        public string Type { get; set; }
        
        public string Algorithm { get; set; }


        private Dictionary<string, string> claimsDictionary;

        private string OriginalToken { get; set; }
        private bool WasInbound { get; set; }
        private string[] OriginalParts { get; set; }

        public static JWTToken Parse(string encodedToken)
        {
            return DecodeToken(encodedToken);
        }

        private static JWTToken DecodeToken(string encodedToken)
        {


            var parts = encodedToken.Split('.');
            if (parts.Length != 3)
            {
                throw new ArgumentException("The JWT token should have 3 parts separated by a '.' character. This one didn't");
            }

            var headerString = parts[0];

            headerString = Encoding.UTF8.GetString(Convert.FromBase64String(headerString));

            var header = JsonConvert.DeserializeObject<JWTHeader>(headerString);

            var payloadString = parts[1];
            payloadString = Encoding.UTF8.GetString(Convert.FromBase64String(payloadString));

            var claimsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(payloadString);

            JWTToken token = new JWTToken { Type= header.typ, Algorithm = header.alg, claimsDictionary = claimsDictionary, Signature = parts[2], OriginalToken = encodedToken, OriginalParts = parts };
            
            token.WasInbound = true;

            return token;

        }



        public string GenerateToken(string secret)
        {
            if (WasInbound) return OriginalToken;
            
            StringBuilder sb = new StringBuilder();
            OriginalParts = new string[3];

            OriginalParts[0] = $"{{typ:\"{Type}\", alg=\"{Algorithm}\"}}";
            sb.Append(Convert.ToBase64String(Encoding.UTF8.GetBytes(OriginalParts[0])));

            string hash; 
            switch(Algorithm)
            {
                case "HS256":
                    hash = Convert.ToBase64String(HMACSHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())));
                    break;
                case "none":
                    hash = string.Empty;
                    break;
                default:
                    throw new InvalidOperationException("You are generating a token string without setting the Algorithm properly");
            }
            sb.Append(".").Append(hash);
            
            return sb.ToString();

        }

        internal class JWTHeader
        {
            internal string alg;
            internal string typ;
        }
    }
}
