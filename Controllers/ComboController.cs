using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ComboController : ControllerBase
   {
    private static List<Combo> combos = new List<Combo>();

    private readonly IComboRepository _repository;

    public ComboController(IComboRepository repository)
        {
            _repository = repository;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Combo>>> GetCombos()
        {
            var combos = await _repository.GetAllAsync();
            return Ok(combos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Combo>> GetCombo(int id)
        {
            var combo = await _repository.GetByIdAsync(id);
            if (combo == null)
            {
                return NotFound();
            }
            return Ok(combo);
        }

        [HttpPost]
        public async Task<ActionResult<Combo>> CreateCombo(Combo combo)
        {
            await _repository.AddAsync(combo);
            return CreatedAtAction(nameof(GetCombo), new { id = combo.Id }, combo);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCombo(int id, Combo updatedCombo)
        {
            var existingCombo = await _repository.GetByIdAsync(id);
            if (existingCombo == null)
            {
                return NotFound();
            }

            // Actualizar el combo existente
            existingCombo.PlatoPrincipal = updatedCombo.PlatoPrincipal;
            existingCombo.Bebida = updatedCombo.Bebida;
            existingCombo.Postre = updatedCombo.Postre;
            existingCombo.Descuento = updatedCombo.Descuento;

            await _repository.UpdateAsync(existingCombo);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteCombo(int id)
       {
           var combo = await _repository.GetByIdAsync(id);
           if (combo == null)
           {
               return NotFound();
           }
           await _repository.DeleteAsync(id);
           return NoContent();
       }

   }
}