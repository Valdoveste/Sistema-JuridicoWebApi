using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;
        public StatusController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetAllStatus() => Ok(await _sistemaJuridicoDbContext.PROCESSO_STATUS.ToListAsync());

        [HttpPost("add-status")]
        public async Task<IActionResult> AddStatus([FromBody] PROCESSO_STATUS statusRequest)
        {
            statusRequest.ID = Guid.NewGuid();
            await _sistemaJuridicoDbContext.PROCESSO_STATUS.AddAsync(statusRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(statusRequest);

        }

        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, PROCESSO_STATUS updatestatusRequest)
        {
            var status = await _sistemaJuridicoDbContext.PROCESSO_STATUS.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (status == null)
                return NotFound();

            status.STATUS = updatestatusRequest.STATUS;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(status);
        }

        [HttpDelete("delete-status/{id}")]
        public async Task<IActionResult> DeleteStatus([FromRoute] Guid id)
        {
            var status = await _sistemaJuridicoDbContext.PROCESSO_STATUS.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (status == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_STATUS.Remove(status);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(status);
        }

    }
}
