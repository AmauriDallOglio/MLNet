namespace MLNet.Dominio.MLNet.Entidade
{
    public class ModeloML
    {
        public int Id { get; set; }
        public string NomeModelo { get; set; } = string.Empty;
        public byte[] DadosModelo { get; set; } = Array.Empty<byte>();
        public DateTime DataTreinamento { get; set; } = DateTime.Now;
        public int Versao { get; set; } = 1;
        public int Quantidade { get; set; }


        public async Task IncluirModeloAsync(byte[] dadosModelo, int versao, int quantidade, CancellationToken cancellationToken)
        {
            ModeloML modeloML = new()
            {
                NomeModelo = "SessaoModel",
                DadosModelo = dadosModelo,
                DataTreinamento = DateTime.Now,
                Versao = versao,
                Quantidade = quantidade
            };
        }

    }
}
