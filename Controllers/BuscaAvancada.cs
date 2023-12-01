using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SistemaJuridicoWebAPI.Data;
using SistemaJuridicoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaJuridicoWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuscaAvancada : Controller
    {

        private readonly SistemaJuridicoDbContext _sistemaJuridicoDbContext;

        public BuscaAvancada(SistemaJuridicoDbContext sistemaJuridicoDbContext)
        {
            _sistemaJuridicoDbContext = sistemaJuridicoDbContext;
        }

        [HttpGet("painel-processos/busca-avancada")]
        public IActionResult SearchProcesso([FromQuery] SEARCH_PARAMETERS_PROCESSO searchParameters)
        {
            var queryResult = _sistemaJuridicoDbContext.PROCESSO
             .Where(x => (searchParameters.NUMERO_PROCESSO == null || x.NUMERO_PROCESSO == searchParameters.NUMERO_PROCESSO)
                      && (searchParameters.FASE == null || x.FASE == searchParameters.FASE)
                      && (searchParameters.AREA_DO_DIREITO == null || x.AREA_DO_DIREITO == searchParameters.AREA_DO_DIREITO)
                      && (searchParameters.PATRONO_RESPONSAVEL == null || x.PATRONO_RESPONSAVEL == searchParameters.PATRONO_RESPONSAVEL)
                      && (searchParameters.STATUS == null || x.STATUS == searchParameters.STATUS)
                      && (searchParameters.TIPO_DE_ACAO == null || x.TIPO_DE_ACAO == searchParameters.TIPO_DE_ACAO)
                      && (searchParameters.PARTE_CONTRARIA == null || EF.Functions.Like(x.PARTE_CONTRARIA, "%" + searchParameters.PARTE_CONTRARIA + "%")))
             .OrderByDescending(x => x.DATA_CADASTRO_PROCESSO)
             .ToList();

            return Ok(queryResult);
        }

    }
}

