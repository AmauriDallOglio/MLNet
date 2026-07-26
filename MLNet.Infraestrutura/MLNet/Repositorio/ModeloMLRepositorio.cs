using Microsoft.EntityFrameworkCore;
using MLNet.Dominio.MLNet.Entidade;
using MLNet.Dominio.MLNet.InterfaceRepositorio;
using MLNet.Infraestrutura.MLNet.Contexto;

namespace MLNet.Infraestrutura.MLNet.Repositorio
{
    public class ModeloMLRepositorio : GenericoCommandRepositorio<ModeloML>, IModeloMLRepositorio
    {
        private readonly CommandContexto _CommandContexto;
        public ModeloMLRepositorio(CommandContexto dbContext) : base(dbContext)
        {
            _CommandContexto = dbContext;
        }



        public async Task<ModeloML?> ObterUltimoAsync(CancellationToken cancellationToken)
        {
            var ultimoModelo = await _CommandContexto.ModeloML.OrderByDescending(m => m.DataTreinamento).FirstOrDefaultAsync(cancellationToken);
            return ultimoModelo;
        }




    }
}

