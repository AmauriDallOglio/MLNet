using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MLNet.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLNet.Infraestrutura.Mapeamento
{
    public class ModeloMLMapeamento : IEntityTypeConfiguration<ModeloML>
    {
        public void Configure(EntityTypeBuilder<ModeloML> builder)
        {
            builder.ToTable("ModeloML");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.NomeModelo).IsRequired().HasMaxLength(200);
            builder.Property(m => m.DadosModelo).IsRequired();
            builder.Property(m => m.DataTreinamento).HasColumnType("datetime");
            builder.Property(m => m.Versao).HasDefaultValue(1);
            builder.Property(m => m.Quantidade).IsRequired(); // define como obrigatório
        }
    }
}

/*
 * 
 
 
CREATE TABLE ModeloML (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NomeModelo NVARCHAR(200) NOT NULL,
    DadosModelo VARBINARY(MAX) NOT NULL,
    DataTreinamento DATETIME NOT NULL DEFAULT GETDATE(),
    Versao INT NOT NULL DEFAULT 1,
    Quantidade INT NOT NULL DEFAULT 0
);

 * 
 */