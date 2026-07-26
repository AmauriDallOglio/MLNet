using MLNet.Dominio.MLNet.Entidade;

namespace MLNet.Aplicacao.Rotas.MLNetRota
{
    public class ObterTreinamentoResponse
    {
        public int Id { get; set; }
        public string NomeModelo { get; set; } = string.Empty;
        public byte[] DadosModelo { get; set; } = Array.Empty<byte>();
        public DateTime DataTreinamento { get; set; } = DateTime.Now;
        public int Versao { get; set; } = 1;
        public int Quantidade { get; set; }

        public static ObterTreinamentoResponse Criar(ModeloML? modeloML) 
        {
            return new ObterTreinamentoResponse
            {
                Id = modeloML?.Id ?? 0,
                NomeModelo = modeloML?.NomeModelo ?? string.Empty,
                DadosModelo = modeloML?.DadosModelo ?? Array.Empty<byte>(),
                DataTreinamento = modeloML?.DataTreinamento ?? DateTime.Now,
                Versao = modeloML?.Versao ?? 1,
                Quantidade = modeloML?.Quantidade ?? 0          
            };

        }
    }
}
