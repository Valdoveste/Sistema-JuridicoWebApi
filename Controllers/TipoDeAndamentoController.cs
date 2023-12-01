using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class TipoDeAndamentoController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public TipoDeAndamentoController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("tipo-de-andamento")]
        public async Task<IActionResult> GetAllTipoDeAndamento() => Ok(await _sistemaJuridicoDbContext.PROCESSO_TIPO_DE_ANDAMENTO.ToListAsync());

        [HttpPost("add-tipo-de-andamento")]
        public async Task<IActionResult> AddTipoDeAndamento([FromBody] PROCESSO_TIPO_DE_ANDAMENTO tipoDeAndamentoRequest)
        {
            tipoDeAndamentoRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_TIPO_DE_ANDAMENTO.AddAsync(tipoDeAndamentoRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(tipoDeAndamentoRequest);
        }

        [HttpPut("update-tipo-de-andamento/{id}")]
        public async Task<IActionResult> UpdateTipoDeAndamento([FromRoute] Guid id, PROCESSO_TIPO_DE_ANDAMENTO updateTipoDeAndamentoRequest)
        {
            var tipoDeAndamento = await _sistemaJuridicoDbContext.PROCESSO_TIPO_DE_ANDAMENTO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (tipoDeAndamento == null)
                return NotFound();

            tipoDeAndamento.TIPO_DE_ANDAMENTO = updateTipoDeAndamentoRequest.TIPO_DE_ANDAMENTO;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(tipoDeAndamento);
        }

        [HttpDelete("delete-tipo-de-andamento/{id}")]
        public async Task<IActionResult> DeleteTipoDeAndamento([FromRoute] Guid id)
        {
            var tipoDeAndamento = await _sistemaJuridicoDbContext.PROCESSO_TIPO_DE_ANDAMENTO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (tipoDeAndamento == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_TIPO_DE_ANDAMENTO.Remove(tipoDeAndamento);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(tipoDeAndamento);
        }
    }
}
