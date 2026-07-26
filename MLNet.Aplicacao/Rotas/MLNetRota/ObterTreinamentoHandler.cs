using Microsoft.ML;
using MLNet.Aplicacao.Rotas.MLNet;
using MLNet.Aplicacao.Util;
using MLNet.Dominio.MLNet.Entidade;
using MLNet.Dominio.MLNet.InterfaceRepositorio;
using MLNet.Dominio.Ollama.InterfaceRepositorioOllama;
using System.Diagnostics;

namespace MLNet.Aplicacao.Rotas.MLNetRota
{
    public class ObterTreinamentoHandler : IContratoBaseHandler<ObterTreinamentoRequest, ResultadoOperacao>
    {
        private readonly IPrintaConsole<GerarTreinamentoHandler> _printaConsole;
        private readonly IModeloMLRepositorio _modeloRepositorio;
        private readonly ISessaoCommandOllamaRepositorio _sessaoRepositorio;
        private readonly MLContext _mlContext;

        public ObterTreinamentoHandler(
            IModeloMLRepositorio modeloRepositorio,
            IPrintaConsole<GerarTreinamentoHandler> printaConsole,
            ISessaoCommandOllamaRepositorio sessaoRepositorio
            )
        {
            _mlContext = new MLContext();
            _modeloRepositorio = modeloRepositorio;
            _printaConsole = printaConsole;
            _sessaoRepositorio = sessaoRepositorio;
        }

        public async Task<ResultadoOperacao> Executar(ObterTreinamentoRequest request, CancellationToken cancellationToken = default)
        {
            var tempo = Stopwatch.StartNew();
            _printaConsole.ImprimirSemCor("Iniciando treinamento");

            ModeloML? ultimoModelo = await _modeloRepositorio.ObterUltimoAsync(cancellationToken);


            ObterTreinamentoResponse response = ObterTreinamentoResponse.Criar(ultimoModelo);

            tempo.Stop();
            return ResultadoOperacao.GerarSucesso(response);
        }

    }
}
