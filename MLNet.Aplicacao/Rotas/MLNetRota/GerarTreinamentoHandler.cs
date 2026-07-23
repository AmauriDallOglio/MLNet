using MLNet.Aplicacao.Util;
using MLNet.Dominio.MLNet.Entidade;
using MLNet.Dominio.MLNet.InterfaceRepositorio;
using System.Diagnostics;

namespace MLNet.Aplicacao.Rotas.MLNet
{
    public class GerarTreinamentoHandler : IContratoBaseHandler<GerarTreinamentoRequest, ResultadoOperacao>
    {
        private readonly IPrintaConsole<GerarTreinamentoHandler> _printaConsole;
        private readonly IModeloMLRepositorio _modeloRepositorio;
        public GerarTreinamentoHandler(
            IModeloMLRepositorio modeloRepositorio,
            IPrintaConsole<GerarTreinamentoHandler> printaConsole
        )
        {
            _modeloRepositorio = modeloRepositorio;
            _printaConsole = printaConsole;
        }

        public async Task<ResultadoOperacao> Executar(GerarTreinamentoRequest request, CancellationToken cancellationToken = default)
        {
            var tempo = Stopwatch.StartNew();
            _printaConsole.ImprimirSemCor("Iniciando treinamento");

            ModeloML? ultimoModelo = await _modeloRepositorio.ObterUltimoAsync(cancellationToken);
      
            if (ultimoModelo != null)
            {
                using var ms = new MemoryStream(ultimoModelo.DadosModelo);
                await ultimoModelo.IncluirModeloAsync(ultimoModelo.DadosModelo, ultimoModelo.Versao, ultimoModelo.Quantidade, cancellationToken);


            }

 
 

            GerarTreinamentoResponse resposta = new GerarTreinamentoResponse();

            tempo.Stop();
            return ResultadoOperacao.GerarSucesso(resposta);

            //if (!string.IsNullOrEmpty(resposta))
            //{
            //    return ResultadoOperacao.GerarSucesso(resposta);
            //}
            //else
            //{
            //    return ResultadoOperacao.GerarErro("Não foi possível gerar resposta.", 500);
            //}
        }



    }
}
