using api.Modles;

namespace api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}