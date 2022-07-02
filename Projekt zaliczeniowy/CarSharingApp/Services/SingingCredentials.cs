using Microsoft.IdentityModel.Tokens;

namespace CarSharingApp.Services
{
    internal class SingingCredentials
    {
        private SymmetricSecurityKey key;
        private string hmacSha256;

        public SingingCredentials(SymmetricSecurityKey key, string hmacSha256)
        {
            this.key = key;
            this.hmacSha256 = hmacSha256;
        }
    }
}