using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Authorize]
    [Controller]
    [Route("api/[controller]")]

    public class AcordoController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public AcordoController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("acordo")]
        public async Task<IActionResult> GetAllAcordo() => Ok(await _sistemaJuridicoDbContext.PROCESSO_ACORDO.ToListAsync());
        
        [HttpGet("processo/all/acordo/{id}")]
        public async Task<IActionResult> GetAllProcessoAcordo([FromRoute] string id)
        {
            var processoAcordo = await _sistemaJuridicoDbContext.PROCESSO_ACORDO
                .Where(x => x.ID_PROCESSO.Equals(id))
                .OrderByDescending(x => x.DATA_ACORDO)
                .ToListAsync();

            return Ok(processoAcordo);
        }

        [HttpGet("processo/acordo/{id}")]
        public async Task<IActionResult> GetProcessAcordo([FromRoute] Guid id)
        {
            var processoAcordo = await _sistemaJuridicoDbContext.PROCESSO_ACORDO
              .FirstOrDefaultAsync(x => x.ID.Equals(id));

            return Ok(processoAcordo);
        }

        [HttpDelete("delete-acordo/{id}")]
        public async Task<IActionResult> DeleteProcessAcordo([FromRoute] Guid id)
        {
            var processoAcordo = await _sistemaJuridicoDbContext.PROCESSO_ACORDO
              .FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (processoAcordo == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_ACORDO.Remove(processoAcordo);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(processoAcordo);
        }

        [HttpPut("update-acordo/{id}")]
        public async Task<ActionResult> UpdateProcessAcordo([FromBody] PROCESSO_ACORDO acordoUpdateRequest, [FromRoute] Guid id)
        {
            var processoAcordo = await _sistemaJuridicoDbContext.PROCESSO_ACORDO
              .FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (processoAcordo == null)
                return NotFound();

            processoAcordo.VALOR_ACORDO = acordoUpdateRequest.VALOR_ACORDO;
            processoAcordo.CONDICOES_TENTATIVA_DE_ACORDO = acordoUpdateRequest.CONDICOES_TENTATIVA_DE_ACORDO;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(processoAcordo);
        }

        [HttpPost("add-acordo")]
        public async Task<IActionResult> AddAcordo([FromBody] PROCESSO_ACORDO acordoRequest)
        {
            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            acordoRequest.DATA_ACORDO = TimeZoneInfo.ConvertTime(DateTime.Now, brazilTimeZone).ToString("dd/MM/yyyyTHH:mm:ss");

            acordoRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_ACORDO.AddAsync(acordoRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
