using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.DTOs;
using RestauranteAPI.Services; 

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

     
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
           
            var token = await _authService.LoginAsync(loginDto);

           
            if (token == null)
            {
                return Unauthorized(new { message = "Email o contraseña incorrectos" });
            }

           
            return Ok(new { token = token });
        }
    }
}