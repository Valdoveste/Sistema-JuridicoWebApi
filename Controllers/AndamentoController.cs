using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class AndamentoController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public AndamentoController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("andamento")]
        public async Task<IActionResult> GetAllAndamento() => Ok(await _sistemaJuridicoDbContext.PROCESSO_ANDAMENTO.ToListAsync());

        [HttpGet("processo/all/andamento/{id}")]
        public async Task<IActionResult> GetAllProcessoAndamento([FromRoute] string id)
        {
            var processoAndamento = await _sistemaJuridicoDbContext.PROCESSO_ANDAMENTO
                .Where(x => x.ID_PROCESSO.Equals(id))
                .OrderByDescending(x => x.DATA_ANDAMENTO)
                .ToListAsync();

            return Ok(processoAndamento);
        }

        [HttpGet("processo/andamento/{id}")]
        public async Task<IActionResult> GetProcessoAndamento([FromRoute] Guid id)
        {
            var andamentoRequest = await _sistemaJuridicoDbContext.PROCESSO_ANDAMENTO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            return Ok(andamentoRequest);
        }

        [HttpPost("add-andamento")]
        public async Task<IActionResult> AddAndamento([FromBody] PROCESSO_ANDAMENTO andamentoRequest)
        {
            andamentoRequest.ID = Guid.NewGuid();

            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            andamentoRequest.DATA_CADASTRO = TimeZoneInfo.ConvertTime(DateTime.Now, brazilTimeZone).ToString("dd/MM/yyyyTHH:mm:ss");
            andamentoRequest.DATA_ANDAMENTO = TimeZoneInfo.ConvertTime(DateTime.Now, brazilTimeZone).ToString("dd/MM/yyyyTHH:mm:ss");

            await _sistemaJuridicoDbContext.PROCESSO_ANDAMENTO.AddAsync(andamentoRequest);

            var processoResquest = await _sistemaJuridicoDbContext.PROCESSO
                .FirstOrDefaultAsync(x => (x.ID_PROCESSO.ToString().Equals(andamentoRequest.ID_PROCESSO)));

            processoResquest.DATA_ULTIMO_ANDAMENTO = andamentoRequest.DATA_ANDAMENTO;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
