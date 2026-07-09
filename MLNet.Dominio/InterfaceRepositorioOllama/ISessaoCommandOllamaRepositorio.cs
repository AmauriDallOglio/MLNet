using MLNet.Dominio.EntidadeOllama;
using MLNet.Dominio.InterfaceRepositorio;

namespace MLNet.Dominio.InterfaceRepositorioOllama
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
