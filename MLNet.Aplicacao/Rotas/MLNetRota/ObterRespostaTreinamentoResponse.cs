namespace MLNet.Aplicacao.Rotas.MLNetRota
{
    public class ObterRespostaTreinamentoResponse
    {
        public string Pergunta { get; set; } = string.Empty;
        public int Versao { get; set; }
        public string Resposta { get; set; } = string.Empty;
    }
}
