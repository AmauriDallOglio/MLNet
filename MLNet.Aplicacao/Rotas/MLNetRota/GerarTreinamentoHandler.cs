using Microsoft.ML;
using MLNet.Aplicacao.Util;
using MLNet.Dominio.MLNet.Entidade;
using MLNet.Dominio.MLNet.InterfaceRepositorio;
using MLNet.Dominio.Ollama.EntidadeOllama;
using MLNet.Dominio.Ollama.InterfaceRepositorioOllama;
using System.Diagnostics;

namespace MLNet.Aplicacao.Rotas.MLNet
{
    public class GerarTreinamentoHandler : IContratoBaseHandler<GerarTreinamentoRequest, ResultadoOperacao>
    {
        private readonly IPrintaConsole<GerarTreinamentoHandler> _printaConsole;
        private readonly IModeloMLRepositorio _modeloRepositorio;
        private readonly ISessaoCommandOllamaRepositorio _sessaoRepositorio;
        private readonly MLContext _mlContext;

        public GerarTreinamentoHandler(
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
            else
            {
                List<Sessao> sessoes = await _sessaoRepositorio.ObterTodosAsync(cancellationToken);
                if (sessoes.Any())
                {
                    List<SessaoDto> dadosSessao = ConverterSessaoParaDto(sessoes);
                    var (modelo, dadosModelo) = TreinarModelo(dadosSessao);
                    await SalvarModeloAsync(modelo, dadosModelo, 1, sessoes.Count, cancellationToken);
                }
            }

 
 

            GerarTreinamentoResponse resposta = new GerarTreinamentoResponse();

            tempo.Stop();
            return ResultadoOperacao.GerarSucesso(resposta);
        }

        private class SessaoDto
        {
            public string Pergunta { get; set; } = string.Empty;
            public string RespostaModelo { get; set; } = string.Empty;
        }

        private List<SessaoDto> ConverterSessaoParaDto(List<Sessao> sessoes)
        {
            var listaDto = new List<SessaoDto>();
            foreach (Sessao sessao in sessoes)
            {
                listaDto.Add(new SessaoDto
                {
                    Pergunta = sessao.Pergunta.ToLowerInvariant().Trim(),
                    RespostaModelo = sessao.RespostaModelo.ToLowerInvariant().Trim()
                });
            }

            return listaDto;
        }

        private (ITransformer modelo, byte[] dadosModelo) TreinarModelo(List<SessaoDto> dadosSessao)
        {
            IDataView data = _mlContext.Data.LoadFromEnumerable(dadosSessao);
            IEstimator<ITransformer> pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SessaoDto.Pergunta))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(SessaoDto.RespostaModelo)))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            ITransformer modelo = pipeline.Fit(data);

            using var ms = new MemoryStream();
            _mlContext.Model.Save(modelo, data.Schema, ms);
            return (modelo, ms.ToArray());
        }

        private async Task SalvarModeloAsync(ITransformer iTransformer, byte[] dadosModelo, int versao, int quantidade, CancellationToken cancellationToken)
        {
            ModeloML modeloML = new()
            {
                NomeModelo = "SessaoModel",
                DadosModelo = dadosModelo,
                DataTreinamento = DateTime.Now,
                Versao = versao,
                Quantidade = quantidade
            };

            await _modeloRepositorio.IncluirAsync(modeloML, cancellationToken);


        }

    }
}
