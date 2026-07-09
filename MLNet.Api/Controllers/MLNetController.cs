using Microsoft.AspNetCore.Mvc;
using MLNet.Aplicacao.Rotas.MLNet;
using MLNet.Aplicacao.Util;

namespace MLNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MLNetController : ControllerBase
    {
        private readonly GerarTreinamentoHandler _GerarTreinamentoHandler;
        public MLNetController( GerarTreinamentoHandler gerarTreinamentoHandler )
        {
            _GerarTreinamentoHandler = gerarTreinamentoHandler;
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
    }
}
