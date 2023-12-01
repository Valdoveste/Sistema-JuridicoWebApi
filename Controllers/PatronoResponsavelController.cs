using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class PatronoResponsavelController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public PatronoResponsavelController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("patrono-responsavel")]
        public async Task<IActionResult> GetAllPatronoResponsavel() => Ok(await _sistemaJuridicoDbContext.PROCESSO_PATRONO_RESPONSAVEL.ToListAsync());

        [HttpGet("patrono-responsavel/{id}")]
        public async Task<IActionResult> GetProcessPatronoResponsavel([FromRoute] Guid id)
        {
            var patronoResponsavel = await _sistemaJuridicoDbContext.PROCESSO_PATRONO_RESPONSAVEL
              .FirstOrDefaultAsync(x => x.ID.Equals(id));

            return Ok(patronoResponsavel);
        }


        [HttpPost("add-patrono-responsavel")]
        public async Task<IActionResult> AddPatronoResponsavel([FromBody] PROCESSO_PATRONO_RESPONSAVEL patronoresponsavelRequest)
        {
            patronoresponsavelRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_PATRONO_RESPONSAVEL.AddAsync(patronoresponsavelRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(patronoresponsavelRequest);
        }

        [HttpPut("update-patrono-responsavel/{id}")]
        public async Task<IActionResult> UpdatePatronoResponsavel([FromRoute] Guid id, PROCESSO_PATRONO_RESPONSAVEL updateFaseRequest)
        {
            var patronoresponsavel = await _sistemaJuridicoDbContext.PROCESSO_PATRONO_RESPONSAVEL.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (patronoresponsavel == null)
                return NotFound();

            patronoresponsavel.PATRONO_RESPONSAVEL = updateFaseRequest.PATRONO_RESPONSAVEL;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(patronoresponsavel);
        }

        [HttpDelete("delete-patrono-responsavel/{id}")]
        public async Task<IActionResult> DeletePatronoResponsavel([FromRoute] Guid id)
        {
            var patronoresponsavel = await _sistemaJuridicoDbContext.PROCESSO_PATRONO_RESPONSAVEL.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (patronoresponsavel == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_PATRONO_RESPONSAVEL.Remove(patronoresponsavel);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(patronoresponsavel);
        }
    }
}
