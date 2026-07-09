using Microsoft.AspNetCore.Mvc;
using MLNet.Aplicacao.Rotas.MLNet;
using MLNet.Dominio.InterfaceRepositorio;
using MLNet.Infraestrutura.Repositorio;

namespace MLNet.Api.Configuracao
{
    public class InjecaoDependenciaConfiguracao
    {
        public static void RegistrarServicos(WebApplicationBuilder builder)
        {

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });



            builder.Services.AddScoped<IModeloMLRepositorio, ModeloMLRepositorio>();
            builder.Services.AddScoped<GerarTreinamentoHandler>();

        }
    }
}
