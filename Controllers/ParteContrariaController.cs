using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class ParteContrariaController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public ParteContrariaController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }


        [HttpGet("parte-contraria")]
        public async Task<IActionResult> GetAllParteContraria() => Ok(await _sistemaJuridicoDbContext.PROCESSO_PARTE_CONTRARIA.ToListAsync());

        [HttpPost("add-parte-contraria")]
        public async Task<IActionResult> AddParteContraria([FromBody] PROCESSO_PARTE_CONTRARIA parteContrariaRequest)
        {

            parteContrariaRequest.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_PARTE_CONTRARIA.AddAsync(parteContrariaRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(parteContrariaRequest);
        }

        [HttpPut("link-parte-contraria/{NUMERO_PROCESSO}")]
        public async Task<IActionResult> LinkParteContraria([FromBody] PROCESSO_PARTE_CONTRARIA parteContrariaRequest, [FromRoute] string NUMERO_PROCESSO)
        {
            var processoController = new ProcessoController(_sistemaJuridicoDbContext);

            var processoRequest = await _sistemaJuridicoDbContext.PROCESSO
              .FirstOrDefaultAsync(x => x.NUMERO_PROCESSO.Equals(NUMERO_PROCESSO));

            if (processoRequest == null)
                return NotFound();

            processoRequest.PARTE_CONTRARIA = parteContrariaRequest.NOME;
            processoRequest.ID_PARTE_CONTRARIA = parteContrariaRequest.ID.ToString();

            var processModifiedInformation = processoController.ProcessChangeLogger();

            var ID_PROCESSO = processoRequest.ID_PROCESSO;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(new { processModifiedInformation, ID_PROCESSO });
        }

        [HttpGet("processo/parte-contraria/{id}")]
        public async Task<IActionResult> GetProcessoParteContraria([FromRoute] Guid id)
        {
            var parteContraria = await _sistemaJuridicoDbContext.PROCESSO_PARTE_CONTRARIA
                .FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (parteContraria == null)
                return NotFound();

            return Ok(parteContraria);
        }

        [HttpPut("update-parte-contraria/{id}")]
        public async Task<IActionResult> UpdateParteContraria([FromRoute] Guid id, PROCESSO_PARTE_CONTRARIA updateParteContrariaRequest)
        {
            var parteContraria = await _sistemaJuridicoDbContext.PROCESSO_PARTE_CONTRARIA.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (parteContraria == null)
                return NotFound();

            parteContraria.NOME = updateParteContrariaRequest.NOME;
            parteContraria.NOME_FANTASIA = updateParteContrariaRequest.NOME_FANTASIA;
            parteContraria.CPF = updateParteContrariaRequest.CPF;
            parteContraria.RG = updateParteContrariaRequest.RG;
            parteContraria.CNPJ = updateParteContrariaRequest.CNPJ;
            parteContraria.ENDERECO = updateParteContrariaRequest.ENDERECO;
            parteContraria.NUMERO = updateParteContrariaRequest.NUMERO;
            parteContraria.CEP = updateParteContrariaRequest.CEP;
            parteContraria.COMPLEMENTO = updateParteContrariaRequest.COMPLEMENTO;
            parteContraria.ESTADO = updateParteContrariaRequest.ESTADO;
            parteContraria.PAIS = updateParteContrariaRequest.PAIS;
            parteContraria.CIDADE = updateParteContrariaRequest.CIDADE;
            parteContraria.OBSERVACAO = updateParteContrariaRequest.OBSERVACAO;
            parteContraria.CARGO = updateParteContrariaRequest.CARGO;
            parteContraria.DATA_ADMISSAO = updateParteContrariaRequest.DATA_ADMISSAO;
            parteContraria.DATA_DEMISSAO = updateParteContrariaRequest.DATA_DEMISSAO;
            parteContraria.ULTIMO_SALARIO = updateParteContrariaRequest.ULTIMO_SALARIO;

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(parteContraria);
        }

        [HttpDelete("delete-parte-contraria/{id}")]
        public async Task<IActionResult> DeleteParteContraria([FromRoute] Guid id)
        {
            var parteContraria = await _sistemaJuridicoDbContext.PROCESSO_PARTE_CONTRARIA.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (parteContraria == null)
                return NotFound();

            _sistemaJuridicoDbContext.PROCESSO_PARTE_CONTRARIA.Remove(parteContraria);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(parteContraria);
        }
    }
}
