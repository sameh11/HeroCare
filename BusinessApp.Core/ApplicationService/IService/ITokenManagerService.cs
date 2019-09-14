using BusinessApp.Core.Entity.Users;

namespace BusinessApp.Core.ApplicationService.IService
{
    public interface ITokenManagerService
    {
        string GenerateJwtToken(object user);
    }
}