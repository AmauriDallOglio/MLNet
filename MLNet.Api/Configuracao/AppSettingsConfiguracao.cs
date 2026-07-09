using Microsoft.EntityFrameworkCore;
using MLNet.Aplicacao.DTO;
using MLNet.Infraestrutura.Contexto;

namespace MLNet.Api.Configuracao
{
    public static class AppSettingsConfiguracao
    {
        public static void Carregar(this IServiceCollection services, IConfigurationRoot configuration)
        {
            AppSettingsDto appSettingsDto = configuration.Get<AppSettingsDto>() ?? new AppSettingsDto();

            appSettingsDto = CarregaBancoDeDados(services, configuration, appSettingsDto);
            services.AddSingleton(appSettingsDto);
        }

        private static AppSettingsDto CarregaBancoDeDados(this IServiceCollection services, IConfigurationRoot configuration, AppSettingsDto appSettingsDto)
        {
            var conexaoCommand = appSettingsDto.ConnectionStrings.ConexaoServidor;
            var conexaoQuery = string.IsNullOrWhiteSpace(appSettingsDto.ConnectionStrings.ConexaoServidorQuery)
                ? conexaoCommand
                : appSettingsDto.ConnectionStrings.ConexaoServidorQuery;

            services.AddDbContext<CommandContexto>(opt => opt.UseSqlServer(conexaoCommand));
            services.AddDbContext<GenericoContexto>(opt => opt.UseSqlServer(conexaoCommand));

            return appSettingsDto;
        }
    }
}
