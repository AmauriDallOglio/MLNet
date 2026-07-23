using Microsoft.EntityFrameworkCore;
using MLNet.Dominio.MLNet.Entidade;
using MLNet.Dominio.Ollama.EntidadeOllama;
using MLNet.Infraestrutura.MLNet.Mapeamento;
using MLNet.Infraestrutura.Ollama.MapeamentoOllama;

namespace MLNet.Infraestrutura.MLNet.Contexto
{
    public class GenericoContexto : DbContext
    {

        public GenericoContexto(DbContextOptions options) : base(options)
        {
        }


        public DbSet<ModeloML> ModeloML { get; set; }

        public DbSet<Sessao> Sessao { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new ModeloMLMapeamento());
            modelBuilder.ApplyConfiguration(new SessaoOllamaMapeamento());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
