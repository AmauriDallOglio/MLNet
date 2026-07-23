using MLNet.Dominio.MLNet.InterfaceRepositorio;
using MLNet.Dominio.Ollama.EntidadeOllama;

namespace MLNet.Dominio.Ollama.InterfaceRepositorioOllama
{
    public interface ISessaoCommandOllamaRepositorio : IGenericoCommandRepositorio<Sessao>
    {
        Task<List<Sessao>> ObterPaginadoAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<List<Sessao>> ObterNovosOuAlteradosAsync(DateTime desde, CancellationToken cancellationToken);
        Task AdicionarAsync(Sessao sessao, CancellationToken cancellationToken);
        Task AtualizarAsync(Sessao sessao, CancellationToken cancellationToken);
        Task<List<string>> ObterPorPerguntaAsync(string pergunta, CancellationToken cancellationToken);
    }
}
