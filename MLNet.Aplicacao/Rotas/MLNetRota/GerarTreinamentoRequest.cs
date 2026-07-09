using MLNet.Aplicacao.Util;

namespace MLNet.Aplicacao.Rotas.MLNet
{
    public class GerarTreinamentoRequest : IRequest<ResultadoOperacao>
    {
        public string Pergunta { get; set; } = string.Empty;
        public string Resposta { get; set; } = string.Empty;
    }
}
