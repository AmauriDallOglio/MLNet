using Microsoft.EntityFrameworkCore;
using MLNet.Dominio.Entidade;
using MLNet.Dominio.EntidadeOllama;
using MLNet.Infraestrutura.Mapeamento;
using MLNet.Infraestrutura.MapeamentoOllama;

namespace MLNet.Infraestrutura.Contexto
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
