using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class PatronoAnteriorController  : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public PatronoAnteriorController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpPost("add-patrono-anterior")]
        public async Task<IActionResult> AddPatronoAnterior([FromBody] PROCESSO_PATRONOS_ANTERIORES patronoAnteriorRequest)
        {

            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            patronoAnteriorRequest.DATA_ALTERACAO = TimeZoneInfo.ConvertTime(DateTime.Now, brazilTimeZone).ToString("dd/MM/yyyyTHH:mm:ss");

            patronoAnteriorRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.AddAsync(patronoAnteriorRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(patronoAnteriorRequest);
        }


        [HttpGet("processo/all/patrono-anterior/{ID_PROCESSO}")]
        public async Task<IActionResult> GetProcessoPatronoAnterior([FromRoute] string ID_PROCESSO)
        {
            var processoPatronoAnterior = await _sistemaJuridicoDbContext.PROCESSO_PATRONOS_ANTERIORES
            .Where(x => x.ID_PROCESSO.Equals(ID_PROCESSO))
            .ToListAsync();

            return Ok(processoPatronoAnterior);
        }

    }
}
