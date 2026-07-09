using MLNet.Dominio.Entidade;

namespace MLNet.Dominio.InterfaceRepositorio
{
    public interface IModeloMLRepositorio : IGenericoCommandRepositorio<ModeloML>
    {
        Task<ModeloML?> ObterUltimoAsync(CancellationToken cancellationToken);
    }
}
