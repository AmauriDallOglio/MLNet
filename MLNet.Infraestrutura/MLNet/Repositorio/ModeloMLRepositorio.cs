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

        //public async Task SalvarAsync(ModeloML modeloML, CancellationToken cancellationToken)
        //{
        //    var modeloExistente = await _CommandContexto.ModeloML.FirstOrDefaultAsync(m => m.NomeModelo == modeloML.NomeModelo, cancellationToken);
        //    if (modeloExistente != null)
        //    {
        //        modeloExistente.DadosModelo = modeloExistente.DadosModelo;
        //        modeloExistente.DataTreinamento = DateTime.Now;
        //        modeloExistente.Versao++;
        //        _CommandContexto.ModeloML.Update(modeloExistente);
        //    }
        //    else
        //    {
        //        _CommandContexto.ModeloML.Add(modeloML);
        //    }
        //    await _CommandContexto.SaveChangesAsync(cancellationToken);
        //}

        public async Task<ModeloML?> ObterUltimoAsync(CancellationToken cancellationToken)
        {
            var ultimoModelo = await _CommandContexto.ModeloML.OrderByDescending(m => m.DataTreinamento).FirstOrDefaultAsync(cancellationToken);
            return ultimoModelo;
        }




    }
}

