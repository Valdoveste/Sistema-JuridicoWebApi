using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class VaraController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public VaraController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("vara")]
        public async Task<IActionResult> GetAllVara() => Ok(await _sistemaJuridicoDbContext.PROCESSO_VARA.ToListAsync());

        [HttpPost("add-vara")]
        public async Task<IActionResult> AddVara([FromBody] PROCESSO_VARA varaRequest)
        {
            varaRequest.ID = Guid.NewGuid();
            await _sistemaJuridicoDbContext.PROCESSO_VARA.AddAsync(varaRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(varaRequest);

        }

        [HttpPut("update-vara/{id}")]
        public async Task<IActionResult> UpdateVara([FromRoute] Guid id, PROCESSO_VARA updateVaraRequest)
        {
            var vara = await _sistemaJuridicoDbContext.PROCESSO_VARA.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (vara == null)
                return NotFound();

            vara.VARA = updateVaraRequest.VARA;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(vara);
        }

        [HttpDelete("delete-vara/{id}")]
        public async Task<IActionResult> DeleteVara([FromRoute] Guid id)
        {
            var vara = await _sistemaJuridicoDbContext.PROCESSO_VARA.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (vara == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_VARA.Remove(vara);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(vara);
        }
    }
}
