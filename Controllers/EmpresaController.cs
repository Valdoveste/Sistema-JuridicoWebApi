using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class EmpresaController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public EmpresaController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("empresas")]
        public async Task<IActionResult> GetAllEmpresas() => Ok(await _sistemaJuridicoDbContext.PROCESSO_EMPRESAS.ToListAsync());

        [HttpPost("add-empresas")]
        public async Task<IActionResult> AddEmpresas([FromBody] PROCESSO_EMPRESAS empresasRequest)
        {
            empresasRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_EMPRESAS.AddAsync(empresasRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(empresasRequest);
        }

        [HttpPut("update-empresas/{id}")]
        public async Task<IActionResult> UpdateEmpresas([FromRoute] Guid id, PROCESSO_EMPRESAS updateEmpresasRequest)
        {
            var empresas = await _sistemaJuridicoDbContext.PROCESSO_EMPRESAS.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (empresas == null)
                return NotFound();

            empresas.EMPRESA = updateEmpresasRequest.EMPRESA;
            empresas.CPF_CNPJ = updateEmpresasRequest.CPF_CNPJ;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(empresas);
        }

        [HttpDelete("delete-empresas/{id}")]
        public async Task<IActionResult> DeleteEmpresas([FromRoute] Guid id)
        {
            var empresas = await _sistemaJuridicoDbContext.PROCESSO_EMPRESAS.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (empresas == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_EMPRESAS.Remove(empresas);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(empresas);
        }
    }
}
