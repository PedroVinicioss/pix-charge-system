using System.Security.Claims;

namespace PixCharge.API.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _accessor;

        public UserContextService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Guid UserId
        {
            get
            {
                var userIdClaim = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
            }
        }
    }

}