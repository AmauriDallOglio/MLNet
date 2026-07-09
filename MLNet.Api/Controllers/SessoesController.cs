using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLNet.Aplicacao.Rotas.SessaoRota;
using MLNet.Aplicacao.Util;
using MLNet.Dominio.InterfaceRepositorioOllama;

namespace MLNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessoesController : ControllerBase
    {

        private readonly ISessaoCommandOllamaRepositorio _ISessaoCommandRepositorio;
        private readonly IContratoBaseHandler<ObterTodosSessaoRequest, ResultadoOperacao> _handler;
        public SessoesController(
             ISessaoCommandOllamaRepositorio iSessaoCommandRepositorio,
             IContratoBaseHandler<ObterTodosSessaoRequest, ResultadoOperacao> handler)
        {
            _ISessaoCommandRepositorio = iSessaoCommandRepositorio;
            _handler = handler;
        }



        [AllowAnonymous]
        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos([FromQuery] ObterTodosSessaoRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _handler.Executar(request, cancellationToken);
            return Ok(resultado);
        }
    }
}
