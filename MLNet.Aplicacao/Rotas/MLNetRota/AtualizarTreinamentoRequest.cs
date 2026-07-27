using MLNet.Aplicacao.Util;

namespace MLNet.Aplicacao.Rotas.MLNetRota
{
    public class AtualizarTreinamentoRequest : IRequest<ResultadoOperacao>
    {
        public int Versao { get; set; }
        public bool UsarVersaoAnterior { get; set; } //UsarVersaoAnterior = true ( SdcaMaximumEntropy ) false ( LbfgsMaximumEntropy )

    }
}
