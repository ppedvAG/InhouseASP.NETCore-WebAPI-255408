using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}