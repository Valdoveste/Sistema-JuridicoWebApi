using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class FaseController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public FaseController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("fase")]
        public async Task<IActionResult> GetAllFase() => Ok(await _sistemaJuridicoDbContext.PROCESSO_FASE.ToListAsync());

        [HttpPost("add-fase")]
        public async Task<IActionResult> AddAmbito([FromBody] PROCESSO_FASE faseRequest)
        {
            faseRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_FASE.AddAsync(faseRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(faseRequest);

        }

        [HttpPut("update-fase/{id}")]
        public async Task<IActionResult> UpdateAmbito([FromRoute] Guid id, PROCESSO_FASE updateAmbitoRequest)
        {
            var fase = await _sistemaJuridicoDbContext.PROCESSO_FASE.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (fase == null)
                return NotFound();

            fase.FASE = updateAmbitoRequest.FASE;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(fase);
        }

        [HttpDelete("delete-fase/{id}")]
        public async Task<IActionResult> DeleteAmbito([FromRoute] Guid id)
        {
            var fase = await _sistemaJuridicoDbContext.PROCESSO_FASE.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (fase == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_FASE.Remove(fase);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(fase);
        }
    }
}
