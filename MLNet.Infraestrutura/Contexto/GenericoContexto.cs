using Microsoft.EntityFrameworkCore;
using MLNet.Dominio.Entidade;
using MLNet.Infraestrutura.Mapeamento;

namespace MLNet.Infraestrutura.Contexto
{
    public class GenericoContexto : DbContext
    {

        public GenericoContexto(DbContextOptions options) : base(options)
        {
        }


        public DbSet<ModeloML> ModeloML { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new ModeloMLMapeamento());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
