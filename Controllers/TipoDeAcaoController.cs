using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class TipoDeAcaoController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public TipoDeAcaoController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("tipo-de-acao")]
        public async Task<IActionResult> GetAllTipoDeAcao() => Ok(await _sistemaJuridicoDbContext.PROCESSO_TIPO_DE_ACAO.ToListAsync());

        [HttpPost("add-tipo-de-acao")]
        public async Task<IActionResult> AddTipoDeAcao([FromBody] PROCESSO_TIPO_DE_ACAO tipoDeAcaoRequest)
        {
            tipoDeAcaoRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_TIPO_DE_ACAO.AddAsync(tipoDeAcaoRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(tipoDeAcaoRequest);
        }

        [HttpPut("update-tipo-de-acao/{id}")]
        public async Task<IActionResult> UpdateTipoDeAcao([FromRoute] Guid id, PROCESSO_TIPO_DE_ACAO updateTipoDeAcaoRequest)
        {
            var tipoDeAcao = await _sistemaJuridicoDbContext.PROCESSO_TIPO_DE_ACAO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (tipoDeAcao == null)
                return NotFound();

            tipoDeAcao.TIPO_DE_ACAO = updateTipoDeAcaoRequest.TIPO_DE_ACAO;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(tipoDeAcao);
        }

        [HttpDelete("delete-tipo-de-acao/{id}")]
        public async Task<IActionResult> DeleteTipoDeAcao([FromRoute] Guid id)
        {
            var tipoDeAcao = await _sistemaJuridicoDbContext.PROCESSO_TIPO_DE_ACAO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (tipoDeAcao == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_TIPO_DE_ACAO.Remove(tipoDeAcao);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(tipoDeAcao);
        }
    }
}
