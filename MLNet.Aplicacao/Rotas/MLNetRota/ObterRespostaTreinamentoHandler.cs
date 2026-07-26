using Microsoft.ML;
using MLNet.Aplicacao.Util;
using MLNet.Dominio.MLNet.Entidade;
using MLNet.Dominio.MLNet.InterfaceRepositorio;

namespace MLNet.Aplicacao.Rotas.MLNetRota
{
    public class ObterRespostaTreinamentoHandler : IContratoBaseHandler<ObterRespostaTreinamentoRequest, ResultadoOperacao>
    {
        private readonly IModeloMLRepositorio _modeloRepositorio;
        private readonly MLContext _mlContext;

        public ObterRespostaTreinamentoHandler(IModeloMLRepositorio modeloRepositorio)
        {
            _modeloRepositorio = modeloRepositorio;
            _mlContext = new MLContext();
        }

        public async Task<ResultadoOperacao> Executar(ObterRespostaTreinamentoRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Pergunta))
                return ResultadoOperacao.GerarErro("Informe a pergunta.", 400);

            if (request.Versao <= 0)
                return ResultadoOperacao.GerarErro("Informe uma versão válida.", 400);

            ModeloML? modeloML = await _modeloRepositorio.ObterPorVersaoAsync(request.Versao, cancellationToken);
            if (modeloML == null || modeloML.DadosModelo.Length == 0)
                return ResultadoOperacao.GerarErro($"Modelo da versão {request.Versao} não encontrado.", 404);

            string pergunta = request.Pergunta.ToLowerInvariant().Trim();
            using var ms = new MemoryStream(modeloML.DadosModelo);

            ITransformer modelo = _mlContext.Model.Load(ms, out _);
            PredictionEngine<SessaoEntrada, SessaoPredicao> predicaoEngine =
                _mlContext.Model.CreatePredictionEngine<SessaoEntrada, SessaoPredicao>(modelo);

            SessaoPredicao predicao = predicaoEngine.Predict(new SessaoEntrada
            {
                Pergunta = pergunta,
                RespostaModelo = string.Empty
            });

            ObterRespostaTreinamentoResponse response = new()
            {
                Pergunta = pergunta,
                Versao = modeloML.Versao,
                Resposta = predicao.PredictedLabel ?? string.Empty
            };

            return ResultadoOperacao.GerarSucesso(response);
        }

        private class SessaoEntrada
        {
            public string Pergunta { get; set; } = string.Empty;
            public string RespostaModelo { get; set; } = string.Empty;
        }

        private class SessaoPredicao
        {
            public string PredictedLabel { get; set; } = string.Empty;
        }
    }
}
