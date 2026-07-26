using Microsoft.AspNetCore.Mvc;
using MLNet.Aplicacao.Rotas.MLNet;
using MLNet.Aplicacao.Rotas.MLNetRota;
using MLNet.Aplicacao.Util;

namespace MLNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MLNetController : ControllerBase
    {
        private readonly GerarTreinamentoHandler _GerarTreinamentoHandler;
        private readonly ObterTreinamentoHandler _ObterTreinamentoHandler;
        public MLNetController( GerarTreinamentoHandler gerarTreinamentoHandler, ObterTreinamentoHandler obterTreinamentoHandler )
        {
            _GerarTreinamentoHandler = gerarTreinamentoHandler;
            _ObterTreinamentoHandler = obterTreinamentoHandler;
        }



        [HttpGet("GerarTreinamento")]
        public async Task<IActionResult?> GerarTreinamento([FromQuery] GerarTreinamentoRequest request, CancellationToken cancellationToken)
        {
            ResultadoOperacao resultado = await _GerarTreinamentoHandler.Executar(request, cancellationToken);
            if (resultado.Sucesso)
                return Ok(resultado.Resultado);
            else
                return BadRequest(resultado.Mensagem);
        }


        [HttpGet("ObterTreinamento")]
        public async Task<IActionResult?> ObterTreinamento([FromQuery] ObterTreinamentoRequest request, CancellationToken cancellationToken)
        {
            ResultadoOperacao resultado = await _ObterTreinamentoHandler.Executar(request, cancellationToken);
            if (resultado.Sucesso)
                return Ok(resultado.Resultado);
            else
                return BadRequest(resultado.Mensagem);
        }


    }
}
