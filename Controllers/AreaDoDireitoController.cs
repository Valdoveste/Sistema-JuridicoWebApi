using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class AreaDoDireitoController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public AreaDoDireitoController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("area-do-direito")]
        public async Task<IActionResult> GetAllAreaDoDireito() => Ok(await _sistemaJuridicoDbContext.PROCESSO_AREA_DO_DIREITO.ToListAsync());

        [HttpPost("add-area-do-direito")]
        public async Task<IActionResult> AddAreaDoDireito([FromBody] PROCESSO_AREA_DO_DIREITO areaDoDireitoRequest)
        {
            areaDoDireitoRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_AREA_DO_DIREITO.AddAsync(areaDoDireitoRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(areaDoDireitoRequest);

        }

        [HttpPut("update-area-do-direito/{id}")]
        public async Task<IActionResult> UpdateAreaDoDireito([FromRoute] Guid id, PROCESSO_AREA_DO_DIREITO updateAreaDoDireitoRequest)
        {
            var areaDoDireito = await _sistemaJuridicoDbContext.PROCESSO_AREA_DO_DIREITO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (areaDoDireito == null)
                return NotFound();

            areaDoDireito.AREA_DO_DIREITO = updateAreaDoDireitoRequest.AREA_DO_DIREITO;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(areaDoDireito);
        }

        [HttpDelete("delete-area-do-direito/{id}")]
        public async Task<IActionResult> DeleteAreaDoDireito([FromRoute] Guid id)
        {
            var areaDoDireito = await _sistemaJuridicoDbContext.PROCESSO_AREA_DO_DIREITO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (areaDoDireito == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_AREA_DO_DIREITO.Remove(areaDoDireito);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(areaDoDireito);
        }
    }
}
