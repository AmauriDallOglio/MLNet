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
        private readonly ObterRespostaTreinamentoHandler _ObterRespostaTreinamentoHandler;
        private readonly AtualizarTreinamentoHandler _AtualizarTreinamentoHandler;

        public MLNetController(
            GerarTreinamentoHandler gerarTreinamentoHandler,
            ObterTreinamentoHandler obterTreinamentoHandler,
            ObterRespostaTreinamentoHandler obterRespostaTreinamentoHandler,
            AtualizarTreinamentoHandler atualizarTreinamentoHandler)
        {
            _GerarTreinamentoHandler = gerarTreinamentoHandler;
            _ObterTreinamentoHandler = obterTreinamentoHandler;
            _ObterRespostaTreinamentoHandler = obterRespostaTreinamentoHandler;
            _AtualizarTreinamentoHandler = atualizarTreinamentoHandler;
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

        [HttpGet("ObterRespostaTreinamento")]
        public async Task<IActionResult?> ObterRespostaTreinamento([FromQuery] ObterRespostaTreinamentoRequest request, CancellationToken cancellationToken)
        {
            ResultadoOperacao resultado = await _ObterRespostaTreinamentoHandler.Executar(request, cancellationToken);
            if (resultado.Sucesso)
                return Ok(resultado.Resultado);
            else
                return StatusCode(resultado.StatusCodigo ?? 400, resultado.Mensagem);
        }

        [HttpGet("AtualizarTreinamento")]
        public async Task<IActionResult?> AtualizarTreinamento([FromQuery] AtualizarTreinamentoRequest request, CancellationToken cancellationToken)
        {
            ResultadoOperacao resultado = await _AtualizarTreinamentoHandler.Executar(request, cancellationToken);
            if (resultado.Sucesso)
                return Ok(resultado.Resultado);
            else
                return StatusCode(resultado.StatusCodigo ?? 400, resultado.Mensagem);
        }


    }
}
