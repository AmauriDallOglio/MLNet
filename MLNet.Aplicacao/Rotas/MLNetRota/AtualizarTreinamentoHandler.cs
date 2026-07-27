using Microsoft.ML;
using MLNet.Aplicacao.Util;
using MLNet.Dominio.MLNet.Entidade;
using MLNet.Dominio.MLNet.InterfaceRepositorio;
using MLNet.Dominio.Ollama.EntidadeOllama;
using MLNet.Dominio.Ollama.InterfaceRepositorioOllama;
using System.Diagnostics;

namespace MLNet.Aplicacao.Rotas.MLNetRota
{
    public class AtualizarTreinamentoHandler : IContratoBaseHandler<AtualizarTreinamentoRequest, ResultadoOperacao>
    {
        private readonly IPrintaConsole<AtualizarTreinamentoHandler> _printaConsole;
        private readonly IModeloMLRepositorio _modeloRepositorio;
        private readonly ISessaoCommandOllamaRepositorio _sessaoRepositorio;
        private readonly MLContext _mlContext;

        public AtualizarTreinamentoHandler(
            IModeloMLRepositorio modeloRepositorio,
            IPrintaConsole<AtualizarTreinamentoHandler> printaConsole,
            ISessaoCommandOllamaRepositorio sessaoRepositorio)
        {
            _mlContext = new MLContext();
            _modeloRepositorio = modeloRepositorio;
            _printaConsole = printaConsole;
            _sessaoRepositorio = sessaoRepositorio;
        }

        public async Task<ResultadoOperacao> Executar(AtualizarTreinamentoRequest request, CancellationToken cancellationToken = default)
        {
            var tempo = Stopwatch.StartNew();
            _printaConsole.ImprimirSemCor("Iniciando atualizacao de treinamento");

            if (request.Versao <= 0)
                return ResultadoOperacao.GerarErro("Informe uma versão valida.", 400);

            ModeloML? modeloBase = await _modeloRepositorio.ObterPorVersaoAsync(request.Versao, cancellationToken);
            if (modeloBase == null)
                return ResultadoOperacao.GerarErro("Treinamento da versão informada não encontrado.", 404);

            List<Sessao> sessoes = await _sessaoRepositorio.ObterTodosAsync(cancellationToken);
            if (!sessoes.Any())
                return ResultadoOperacao.GerarErro("Não existem sessões para atualizar o treinamento.", 400);

            List<Sessao> sessoesNovasOuAlteradas = await _sessaoRepositorio.ObterNovosOuAlteradosAsync(modeloBase.DataTreinamento, cancellationToken);
            List<SessaoDto> dadosSessao = ConverterSessaoParaDto(sessoes);
            string algoritmo = request.UsarVersaoAnterior ? "SdcaMaximumEntropy" : "LbfgsMaximumEntropy";
            byte[] dadosModelo = TreinarModelo(dadosSessao, request.UsarVersaoAnterior);

            int novaVersao = modeloBase.Versao + 1;
            ModeloML modeloAtualizado = new()
            {
                NomeModelo = modeloBase.NomeModelo,
                DadosModelo = dadosModelo,
                DataTreinamento = DateTime.Now,
                Versao = novaVersao,
                Quantidade = sessoes.Count
            };

            await _modeloRepositorio.IncluirAsync(modeloAtualizado, cancellationToken);

            tempo.Stop();
            AtualizarTreinamentoResponse response = new()
            {
                VersaoBase = modeloBase.Versao,
                NovaVersao = novaVersao,
                QuantidadeAnterior = modeloBase.Quantidade,
                QuantidadeSessoesNovasOuAlteradas = sessoesNovasOuAlteradas.Count,
                QuantidadeTreinamento = sessoes.Count,
                DataTreinamento = modeloAtualizado.DataTreinamento,
                Algoritmo = algoritmo
            };

            return ResultadoOperacao.GerarSucesso(response);
        }

        private class SessaoDto
        {
            public string Pergunta { get; set; } = string.Empty;
            public string RespostaModelo { get; set; } = string.Empty;
        }

        private static List<SessaoDto> ConverterSessaoParaDto(List<Sessao> sessoes)
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

        private byte[] TreinarModelo(List<SessaoDto> dadosSessao, bool usarVersaoAnterior)
        {
            IDataView data = _mlContext.Data.LoadFromEnumerable(dadosSessao);
            IEstimator<ITransformer> pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SessaoDto.Pergunta))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(SessaoDto.RespostaModelo)));

            //SdcaMaximumEntropy: Atualização precisa retreinar usando o dataset completo das sessões. É mais correto para evitar o modelo possa “esquecer” padrões antigos.
            //LbfgsMaximumEntropy: Carrega o modelo da versão anterior, extrair os parâmetros e usar as sessões novas como base para um retreinamento incremental.
            pipeline = usarVersaoAnterior
                ? pipeline.Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                : pipeline.Append(_mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy("Label", "Features"));

            pipeline = pipeline.Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            ITransformer modelo = pipeline.Fit(data);

            using var ms = new MemoryStream();
            _mlContext.Model.Save(modelo, data.Schema, ms);
            return ms.ToArray();
        }
    }
}
