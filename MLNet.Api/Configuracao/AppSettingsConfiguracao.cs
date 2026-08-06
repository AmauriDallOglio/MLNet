using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MLNet.Aplicacao.DTO;
using MLNet.Infraestrutura.MLNet.Contexto;
using MLNet.Infraestrutura.Ollama.ContextoOllama;

namespace MLNet.Api.Configuracao
{
    public static class AppSettingsConfiguracao
    {
        public static void Carregar(this IServiceCollection services, IConfigurationRoot configuration)
        {
            ////classe que receber AppSettingsDto via injeção de dependência terá exatamente esse objeto, sem suporte a reload on change, ao reiniciar o app ele carrega os novos valores do Azure.
            //AppSettingsDto appSettingsDto = configuration.Get<AppSettingsDto>() ?? new AppSettingsDto();
            //services.AddSingleton(appSettingsDto);

            //é atualizado automaticamente se o arquivo appsettings.json mudar em tempo de execução, não precisa reiniciar, os valores mudam automaticamente..
            services.Configure<AppSettingsDto>(configuration);

            CarregaBancoDeDados(services);
            services.RegistrarRateLimit(configuration.Get<AppSettingsDto>() ?? new AppSettingsDto());

 
 
        }

        private static AppSettingsDto CarregaBancoDeDados(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var monitor = provider.GetRequiredService<IOptionsMonitor<AppSettingsDto>>();
            AppSettingsDto appSettingsDto = monitor.CurrentValue;

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

        public static void AtivarAppSettinngsConfiguracao(this WebApplication app)
        {
            // Resolve o monitor para ativar o OnChange
            var monitor = app.Services.GetRequiredService<IOptionsMonitor<AppSettingsDto>>();

            monitor.OnChange(settings =>
            {
                Console.WriteLine($"[CONFIG] AppSettingsDto alterado em {DateTime.Now}");
                Console.WriteLine($"Nova API Key: {settings.Seguranca.ApiKey}");
                Console.WriteLine($"RateLimit habilitado: {settings.RateLimit.Habilitado}");
            });

            // Configuração dinâmica do RateLimiter
            if (monitor.CurrentValue.RateLimit.Habilitado)
                app.UseRateLimiter();
        }

    }
}
