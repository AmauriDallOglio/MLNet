using MLNet.Aplicacao.Util;

namespace MLNet.Aplicacao.Rotas.MLNetRota
{
    public class ObterRespostaTreinamentoRequest : IRequest<ResultadoOperacao>
    {
        public string Pergunta { get; set; } = string.Empty;
        public int Versao { get; set; } = 1;
    }
}
