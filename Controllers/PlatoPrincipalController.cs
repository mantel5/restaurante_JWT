using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 
using RestauranteAPI.Models;
using RestauranteAPI.Services;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatoPrincipalController : ControllerBase
    {
        private readonly IPlatoPrincipalService _service;

        public PlatoPrincipalController(IPlatoPrincipalService service)
        {
            _service = service;
        }

        // GET: Todo el mundo puede ver la carta (Sin candado)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatoPrincipal>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // POST: Solo el Admin puede crear platos nuevos
        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult> Create(PlatoPrincipal plato)
        {
            await _service.AddAsync(plato); 
            return Ok();
        }

        // DELETE: Solo el Admin puede borrar
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}