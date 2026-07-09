using Microsoft.AspNetCore.Mvc;
using MLNet.Aplicacao.Rotas.MLNet;
using MLNet.Aplicacao.Rotas.SessaoRota;
using MLNet.Aplicacao.Util;
using MLNet.Dominio.InterfaceRepositorio;
using MLNet.Dominio.InterfaceRepositorioOllama;
using MLNet.Infraestrutura.Repositorio;
using MLNet.Infraestrutura.RepositorioOllama;

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



            //Aplicacao
            builder.Services.AddScoped<GerarTreinamentoHandler>();
            builder.Services.AddScoped<IContratoBaseHandler<GerarTreinamentoRequest, ResultadoOperacao>, GerarTreinamentoHandler>();
            builder.Services.AddSingleton(typeof(IPrintaConsole<>), typeof(PrintaConsole<>));
            builder.Services.AddScoped<IContratoBaseHandler<ObterTodosSessaoRequest, ResultadoOperacao>, ObterTodosSessaoHandler>();

            //Dominio
            builder.Services.AddScoped<IModeloMLRepositorio, ModeloMLRepositorio>();
            builder.Services.AddScoped<ISessaoCommandOllamaRepositorio, SessaoCommandOllamaRepositorio>();
 



            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });



        }
    }
}
