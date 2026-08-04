using Microsoft.EntityFrameworkCore;
using MLNet.Aplicacao.DTO;
using MLNet.Infraestrutura.MLNet.Contexto;
using MLNet.Infraestrutura.Ollama.ContextoOllama;

namespace MLNet.Api.Configuracao
{
    public static class AppSettingsConfiguracao
    {
        public static void Carregar(this IServiceCollection services, IConfigurationRoot configuration)
        {
            //classe que receber AppSettingsDto via injeção de dependência terá exatamente esse objeto, sem suporte a reload on change.
            AppSettingsDto appSettingsDto = configuration.Get<AppSettingsDto>() ?? new AppSettingsDto();
            services.AddSingleton(appSettingsDto);

            //é atualizado automaticamente se o arquivo appsettings.json mudar em tempo de execução.
            services.Configure<AppSettingsDto>(configuration);

            appSettingsDto = CarregaBancoDeDados(services, configuration, appSettingsDto);
            services.RegistrarRateLimit(appSettingsDto);
            services.AddSingleton(appSettingsDto);
        }

        private static AppSettingsDto CarregaBancoDeDados(this IServiceCollection services, IConfigurationRoot configuration, AppSettingsDto appSettingsDto)
        {
            var conexaoCommand = appSettingsDto.ConnectionStrings.ConexaoServidor;
            var conexaoQuery = string.IsNullOrWhiteSpace(appSettingsDto.ConnectionStrings.ConexaoServidorQuery)
                ? conexaoCommand
                : appSettingsDto.ConnectionStrings.ConexaoServidorQuery;
            var conexaoOllama = string.IsNullOrWhiteSpace(appSettingsDto.ConnectionStrings.ConexaoServidorOllama)
                ? conexaoCommand
                : appSettingsDto.ConnectionStrings.ConexaoServidorOllama;

            services.AddDbContext<CommandContexto>(opt => opt.UseSqlServer(conexaoCommand));
            services.AddDbContext<GenericoContexto>(opt => opt.UseSqlServer(conexaoCommand));
            services.AddDbContext<CommandOllamaContexto>(opt => opt.UseSqlServer(conexaoOllama));

            return appSettingsDto;
        }
    }
}
