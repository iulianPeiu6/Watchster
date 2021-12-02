using Watchster.Domain.Entities;

namespace Watchster.Jwt.Services.Interfaces
{
    public interface IJwtService
    {
        string generateToken(User command);
    }
}
