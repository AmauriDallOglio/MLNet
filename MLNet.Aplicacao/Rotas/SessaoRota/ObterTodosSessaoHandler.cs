using MLNet.Aplicacao.Util;
using MLNet.Dominio.Ollama.InterfaceRepositorioOllama;

namespace MLNet.Aplicacao.Rotas.SessaoRota
{
    public class ObterTodosSessaoHandler : IContratoBaseHandler<ObterTodosSessaoRequest, ResultadoOperacao>
    {
        private readonly ISessaoCommandOllamaRepositorio _sessaoRepositorio;

        public ObterTodosSessaoHandler(ISessaoCommandOllamaRepositorio sessaoRepositorio)
        {
            _sessaoRepositorio = sessaoRepositorio;
        }

        public async Task<ResultadoOperacao> Executar(ObterTodosSessaoRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                int page = request.Page < 1 ? 1 : request.Page;
                int pageSize = request.PageSize < 1 ? 20 : request.PageSize;
                var sessoes = await _sessaoRepositorio.ObterPaginadoAsync(page, pageSize, cancellationToken);
                var response = ObterTodosSessaoResponse.CriarLista(sessoes, page, pageSize);

                return ResultadoOperacao.GerarSucesso(response);
            }
            catch (Exception ex)
            {
                return ResultadoOperacao.GerarErro($"Erro interno: {ex.Message}", 500);
            }
        }
    }
}
