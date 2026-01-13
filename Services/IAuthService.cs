using System.Security.Claims;
using RestauranteAPI.DTOs; 
namespace RestauranteAPI.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto loginDto);
        bool HasAccessToResource(int requestedUserId, ClaimsPrincipal user);
    }
}