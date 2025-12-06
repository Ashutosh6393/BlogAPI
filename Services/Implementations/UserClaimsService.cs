using System.Security.Claims;
using MegaBlogAPI.Services.Interfaces;

namespace MegaBlogAPI.Services.Implementations
{
    public class UserClaimsService: IUserClaimsService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserClaimsService(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public IEnumerable<Claim> Claims =>
            _httpContextAccessor.HttpContext?.User?.Claims ?? Enumerable.Empty<Claim>();

        public string? UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value;

        public string? Name =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

        public string? Email =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }
}
