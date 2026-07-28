using Microsoft.AspNetCore.Mvc;
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




            builder.Services.AddSingleton(typeof(IPrintaConsole<>), typeof(PrintaConsole<>));

            //Dominio

            builder.Services.AddScoped<IModeloMLRepositorio, ModeloMLRepositorio>();
            builder.Services.AddScoped<ISessaoCommandOllamaRepositorio, SessaoCommandOllamaRepositorio>();
        }
    }
}
