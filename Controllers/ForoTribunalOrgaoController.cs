using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class ForoTribunalOrgaoController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public ForoTribunalOrgaoController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("foro-tribunal-orgao")]
        public async Task<IActionResult> GetAllForoTribunalOrgao() => Ok(await _sistemaJuridicoDbContext.PROCESSO_FORO_TRIBUNAL_ORGAO.ToListAsync());

        [HttpPost("add-foro-tribunal-orgao")]
        public async Task<IActionResult> AddForoTribunalOrgao([FromBody] PROCESSO_FORO_TRIBUNAL_ORGAO foroTribunalOrgaoRequest)
        {
            foroTribunalOrgaoRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_FORO_TRIBUNAL_ORGAO.AddAsync(foroTribunalOrgaoRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(foroTribunalOrgaoRequest);
        }

        [HttpPut("update-foro-tribunal-orgao/{id}")]
        public async Task<IActionResult> UpdateForoTribunalOrgao([FromRoute] Guid id, PROCESSO_FORO_TRIBUNAL_ORGAO updateForoTribunalOrgaoRequest)
        {
            var foroTribunalOrgao = await _sistemaJuridicoDbContext.PROCESSO_FORO_TRIBUNAL_ORGAO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (foroTribunalOrgao == null)
                return NotFound();

            foroTribunalOrgao.FORO_TRIBUNAL_ORGAO = updateForoTribunalOrgaoRequest.FORO_TRIBUNAL_ORGAO;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(foroTribunalOrgao);
        }

        [HttpDelete("delete-foro-tribunal-orgao/{id}")]
        public async Task<IActionResult> DeleteForoTribunalOrgao([FromRoute] Guid id)
        {
            var foroTribunalOrgao = await _sistemaJuridicoDbContext.PROCESSO_FORO_TRIBUNAL_ORGAO.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (foroTribunalOrgao == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_FORO_TRIBUNAL_ORGAO.Remove(foroTribunalOrgao);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(foroTribunalOrgao);
        }
    }
}
