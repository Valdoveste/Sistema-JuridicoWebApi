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
    public class AmbitoController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public AmbitoController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("ambito")]
        public async Task<IActionResult> GetAllAmbito() => Ok(await _sistemaJuridicoDbContext.PROCESSO_AMBITO.ToListAsync());

        [HttpPost("add-ambito")]
        public async Task<IActionResult> AddAmbito([FromBody] PROCESSO_AMBITO ambitoRequest)
        {
            ambitoRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_AMBITO.AddAsync(ambitoRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(ambitoRequest);

        }

        [HttpPut("update-ambito/{id}")]
        public async Task<IActionResult> UpdateAmbito([FromRoute] Guid id, PROCESSO_AMBITO updateAmbitoRequest)
        {
            var ambito = await _sistemaJuridicoDbContext.PROCESSO_AMBITO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (ambito == null)
                return NotFound();

            ambito.AMBITO = updateAmbitoRequest.AMBITO;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(ambito);
        }

        [HttpDelete("delete-ambito/{id}")]
        public async Task<IActionResult> DeleteAmbito([FromRoute] Guid id)
        {
            var ambito = await _sistemaJuridicoDbContext.PROCESSO_AMBITO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (ambito == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_AMBITO.Remove(ambito);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(ambito);
        }
    }
}
