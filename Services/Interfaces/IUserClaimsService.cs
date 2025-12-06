using System.Security.Claims;

namespace MegaBlogAPI.Services.Interfaces
{
    public interface IUserClaimsService
    {
        string? UserId { get; }
        string? Email { get; }
        string? Name { get; }
        IEnumerable<Claim> Claims { get; }

    }
}
