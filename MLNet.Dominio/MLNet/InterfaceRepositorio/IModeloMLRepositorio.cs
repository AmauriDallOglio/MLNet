using MLNet.Dominio.MLNet.Entidade;

namespace MLNet.Dominio.MLNet.InterfaceRepositorio
{
    public interface IModeloMLRepositorio : IGenericoCommandRepositorio<ModeloML>
    {
        Task<ModeloML?> ObterUltimoAsync(CancellationToken cancellationToken);
    }
}
