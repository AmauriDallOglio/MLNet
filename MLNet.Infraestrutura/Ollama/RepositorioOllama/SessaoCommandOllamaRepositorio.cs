using Microsoft.EntityFrameworkCore;
using MLNet.Dominio.Ollama.EntidadeOllama;
using MLNet.Dominio.Ollama.InterfaceRepositorioOllama;
using MLNet.Infraestrutura.MLNet.Repositorio;
using MLNet.Infraestrutura.Ollama.ContextoOllama;

namespace MLNet.Infraestrutura.Ollama.RepositorioOllama
{
    public class SessaoCommandOllamaRepositorio : GenericoCommandRepositorio<Sessao>, ISessaoCommandOllamaRepositorio
    {
        private readonly CommandOllamaContexto _CommandContexto;
        public SessaoCommandOllamaRepositorio(CommandOllamaContexto dbContext) : base(dbContext)
        {
            _CommandContexto = dbContext;
        }

        public new async Task<List<Sessao>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            return await _CommandContexto.Sessao.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<List<Sessao>> ObterPaginadoAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            int paginaNormalizada = page < 1 ? 1 : page;
            int tamanhoNormalizado = pageSize < 1 ? 20 : pageSize;

            return await _CommandContexto.Sessao
                .AsNoTracking()
                .OrderByDescending(s => s.DataCriacao)
                .Skip((paginaNormalizada - 1) * tamanhoNormalizado)
                .Take(tamanhoNormalizado)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Sessao>> ObterNovosOuAlteradosAsync(DateTime desde, CancellationToken cancellationToken)
        {
            return await _CommandContexto.Sessao.AsNoTracking().Where(s => s.DataCriacao >= desde || s.DataAtualizacao >= desde).ToListAsync(cancellationToken);
        }

        public async Task<List<string>> ObterPorPerguntaAsync(string pergunta, CancellationToken cancellationToken)
        {
            return await _CommandContexto.Sessao
                .AsNoTracking()
                .Where(s => s.Pergunta.Contains(pergunta))
                .OrderByDescending(s => s.DataCriacao)   // mais recente primeiro
                .Select(s => s.RespostaModelo)
                .Distinct()
                .ToListAsync(cancellationToken);
        }


        public async Task AdicionarAsync(Sessao sessao, CancellationToken cancellationToken)
        {
            _CommandContexto.Sessao.Add(sessao);
            await _CommandContexto.SaveChangesAsync(cancellationToken);
        }

        public async Task AtualizarAsync(Sessao sessao, CancellationToken cancellationToken)
        {
            _CommandContexto.Sessao.Update(sessao);
            await _CommandContexto.SaveChangesAsync(cancellationToken);
        }
    }
}
