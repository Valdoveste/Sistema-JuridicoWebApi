using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SistemaJuridicoWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class ProcessoController : Controller
    {
        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public ProcessoController(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("processo")]
        public async Task<IActionResult> GetAllProcess()
        {
            var processoRequest = await _sistemaJuridicoDbContext.PROCESSO
              .OrderByDescending(x => x.DATA_CADASTRO_PROCESSO)
              .ToListAsync();

            return Ok(processoRequest);
        }

        [HttpGet("processo/{id}")]
        public async Task<IActionResult> GetProcess([FromRoute] Guid id)
        {
            var processo = await _sistemaJuridicoDbContext.PROCESSO.FirstOrDefaultAsync(x => x.ID_PROCESSO.Equals(id));

            if (processo == null)
                return NotFound();

            return Ok(processo);

        }

        [HttpPost("add-processo")]
        public async Task<IActionResult> AddProcess([FromBody] PROCESSO processoRequest)
        {

            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            processoRequest.DATA_CADASTRO_PROCESSO = TimeZoneInfo.ConvertTime(DateTime.Now, brazilTimeZone).ToString("dd/MM/yyyyTHH:mm:ss");

            processoRequest.ID_PROCESSO = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO.AddAsync(processoRequest);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            var folderName = Path.Combine("Resources", processoRequest.ID_PROCESSO.ToString());

            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            return Ok(processoRequest);
        }

        [HttpPut("update-processo/{id}")]
        public async Task<IActionResult> UpdateProcess([FromRoute] Guid id, PROCESSO updateProcessRequest)
        {
            var processo = await _sistemaJuridicoDbContext.PROCESSO
              .FirstOrDefaultAsync(x => x.ID_PROCESSO.Equals(id));

            if (processo == null)
                return NotFound();

            processo.STATUS = updateProcessRequest.STATUS;
            processo.TIPO_DE_ACAO = updateProcessRequest.TIPO_DE_ACAO;
            processo.AREA_DO_DIREITO = updateProcessRequest.AREA_DO_DIREITO;
            processo.AMBITO = updateProcessRequest.AMBITO;
            processo.EMPRESA = updateProcessRequest.EMPRESA;
            processo.ESTADO = updateProcessRequest.ESTADO;
            processo.PAIS = updateProcessRequest.PAIS;
            processo.CIDADE = updateProcessRequest.CIDADE;
            processo.DATA_CITACAO = updateProcessRequest.DATA_CITACAO;
            processo.DATA_DISTRIBUICAO = updateProcessRequest.DATA_DISTRIBUICAO;
            processo.VARA = updateProcessRequest.VARA;
            processo.FORO_TRIBUNAL_ORGAO = updateProcessRequest.FORO_TRIBUNAL_ORGAO;
            processo.FASE = updateProcessRequest.FASE;
            processo.PATRONO_RESPONSAVEL = updateProcessRequest.PATRONO_RESPONSAVEL;
            processo.PARTE_CONTRARIA = updateProcessRequest.PARTE_CONTRARIA;
            processo.TEXTO_DO_OBJETO = updateProcessRequest.TEXTO_DO_OBJETO;
            processo.VALOR_DO_PEDIDO = updateProcessRequest.VALOR_DO_PEDIDO;
            processo.VALOR_INSTANCIA_EXTRAORDINARIA = updateProcessRequest.VALOR_INSTANCIA_EXTRAORDINARIA;
            processo.VALOR_INSTANCIA1 = updateProcessRequest.VALOR_INSTANCIA1;
            processo.VALOR_INSTANCIA2 = updateProcessRequest.VALOR_INSTANCIA2;
            processo.VALOR_INSTANCIA3 = updateProcessRequest.VALOR_INSTANCIA3;

            var processModifiedInformation = ProcessChangeLogger();

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(new { processModifiedInformation, updateProcessRequest });
        }

        public class EncerramentoRequest
        {
            public string MOTIVO_ENCERRAMENTO { get; set; }
        }

        [HttpPut("update-encerramento/processo/{ID_PROCESSO}")]
        public async Task<IActionResult> UpdateEncerramento([FromRoute] Guid ID_PROCESSO, [FromBody] EncerramentoRequest encerramentoRequequest)
        {
            var processo = await _sistemaJuridicoDbContext.PROCESSO
              .FirstOrDefaultAsync(x => x.ID_PROCESSO.Equals(ID_PROCESSO));

            if (processo == null)
                return NotFound();

            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            processo.DATA_ENCERRAMENTO = TimeZoneInfo.ConvertTime(DateTime.Now, brazilTimeZone).ToString("dd/MM/yyyyTHH:mm:ss");

            processo.MOTIVO_ENCERRAMENTO = encerramentoRequequest.MOTIVO_ENCERRAMENTO;

            var processoModifiedInformation = ProcessChangeLogger();

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(new { processoModifiedInformation, processo });
        }

        public class BaixaProvisoriaRequest
        {
            public string MOTIVO_BAIXA_PROVISORIA { get; set; }
        }

        [HttpPut("update-baixa-provisoria/processo/{ID_PROCESSO}")]
        public async Task<IActionResult> UpdateBaixaProvisoria([FromRoute] Guid ID_PROCESSO, [FromBody] BaixaProvisoriaRequest baixaProvisoriaRequequest)
        {
            var processo = await _sistemaJuridicoDbContext.PROCESSO
              .FirstOrDefaultAsync(x => x.ID_PROCESSO.Equals(ID_PROCESSO));

            if (processo == null)
                return NotFound();

            processo.MOTIVO_BAIXA_PROVISORIA = baixaProvisoriaRequequest.MOTIVO_BAIXA_PROVISORIA;

            var processoModifiedInformation = ProcessChangeLogger();

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(new { processoModifiedInformation, processo });
        }

        [HttpGet("process-track-changes")]
        public List<Object> ProcessChangeLogger()
        {
            var changes = _sistemaJuridicoDbContext.ChangeTracker.Entries<PROCESSO>()
          .Where(x => x.State == EntityState.Modified)
          .Select(x => new
          {
              Original = x.OriginalValues.ToObject(),
              Current = x.CurrentValues.ToObject()
          })
          .ToList();

            List<object> modifiedInformation = new List<object>();

            foreach (var change in changes)
            {
                var original = change.Original as PROCESSO;
                var current = change.Current as PROCESSO;

                if (original != null && current != null)
                {
                    var modifiedProperties = original.GetType()
                        .GetProperties()
                        .Where(property =>
                        {
                            var originalValue = property.GetValue(original);
                            var currentValue = property.GetValue(current);
                            return !object.Equals(originalValue, currentValue);
                        })
                        .ToDictionary(property => property.Name, property => new
                        {
                            VALOR_ORIGINAL = property.GetValue(original),
                            VALOR_ATUAL = property.GetValue(current)
                        });

                    if (modifiedProperties.Any())
                    {
                        modifiedInformation.Add(modifiedProperties);
                    }
                }
            }

            return modifiedInformation;
        }

        [HttpPost("add-log-processo/{id}")]
        public async Task<IActionResult> AddProcessoLog(PROCESSO_LOG_ALTERACOES logProcesso)
        {
            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            logProcesso.DATA_ALTERACAO = TimeZoneInfo.ConvertTime(DateTime.Now, brazilTimeZone).ToString("dd/MM/yyyyTHH:mm:ss");

            logProcesso.ID = Guid.NewGuid();

            await _sistemaJuridicoDbContext.PROCESSO_LOG_ALTERACOES.AddAsync(logProcesso);

            await _sistemaJuridicoDbContext.SaveChangesAsync();

            return Ok(logProcesso);
        }

        [HttpGet("get-log-processo/{id}")]
        public async Task<IActionResult> GetProcessoLog([FromRoute] string id)
        {
            var logProcesso = await _sistemaJuridicoDbContext.PROCESSO_LOG_ALTERACOES
           .Where(x => x.ID_PROCESSO.Equals(id))
           .OrderByDescending(x => x.DATA_ALTERACAO)
           .ToListAsync();

            return Ok(logProcesso);
        }

    }
}
