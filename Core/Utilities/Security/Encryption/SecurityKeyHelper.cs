using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey) // securityKey: WebApi'deki appsettings.json'daki securityKey'e karşılık gelir.
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)); // burada bir token (anahtar) oluşturduk. Sisteme giriş yapmayı sağlayan anahtar.
        }
    }
}
