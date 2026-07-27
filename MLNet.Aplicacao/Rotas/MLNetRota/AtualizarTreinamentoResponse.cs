namespace MLNet.Aplicacao.Rotas.MLNetRota
{
    public class AtualizarTreinamentoResponse
    {
        public int VersaoBase { get; set; }
        public int NovaVersao { get; set; }
        public int QuantidadeAnterior { get; set; }
        public int QuantidadeSessoesNovasOuAlteradas { get; set; }
        public int QuantidadeTreinamento { get; set; }
        public DateTime DataTreinamento { get; set; }
        public string Algoritmo { get; set; } = string.Empty;
    }
}
