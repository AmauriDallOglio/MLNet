using Microsoft.AspNetCore.Mvc;
using MLNet.Aplicacao.Rotas.MLNet;
using MLNet.Aplicacao.Rotas.MLNetRota;
using MLNet.Aplicacao.Rotas.SessaoRota;
using MLNet.Aplicacao.Util;
using MLNet.Dominio.MLNet.InterfaceRepositorio;
using MLNet.Dominio.Ollama.InterfaceRepositorioOllama;
using MLNet.Infraestrutura.MLNet.Repositorio;
using MLNet.Infraestrutura.Ollama.RepositorioOllama;

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
            builder.Services.AddScoped<ObterTreinamentoHandler>();
            builder.Services.AddScoped<IContratoBaseHandler<ObterTreinamentoRequest, ResultadoOperacao>, ObterTreinamentoHandler>();
            builder.Services.AddScoped<ObterRespostaTreinamentoHandler>();
            builder.Services.AddScoped<IContratoBaseHandler<ObterRespostaTreinamentoRequest, ResultadoOperacao>, ObterRespostaTreinamentoHandler>();

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
