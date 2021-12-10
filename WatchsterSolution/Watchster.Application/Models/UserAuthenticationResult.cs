using Watchster.Application.Models;

namespace Watchster.Application.Authentication.Models
{
    public class UserAuthenticationResult
    {
        public UserDetails User { get; set; }

        public string JwtToken { get; set; }

        public string ErrorMessage { get; set; }
    }
}
