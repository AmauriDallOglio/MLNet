using MLNet.Aplicacao.Rotas.MLNet;
using MLNet.Aplicacao.Rotas.MLNetRota;
using MLNet.Aplicacao.Rotas.SessaoRota;
using MLNet.Aplicacao.Util;

namespace MLNet.Api.Configuracao
{
    public static class CqrsConfiguracao
    {
        public static IServiceCollection RegistrarCqrs(this IServiceCollection services)
        {
            services.RegistrarHandler<GerarTreinamentoRequest, GerarTreinamentoHandler>();
            services.RegistrarHandler<ObterTreinamentoRequest, ObterTreinamentoHandler>();
            services.RegistrarHandler<ObterRespostaTreinamentoRequest, ObterRespostaTreinamentoHandler>();
            services.RegistrarHandler<AtualizarTreinamentoRequest, AtualizarTreinamentoHandler>();
            services.RegistrarHandler<ObterTodosSessaoRequest, ObterTodosSessaoHandler>();

            return services;
        }

        private static IServiceCollection RegistrarHandler<TRequest, THandler>(this IServiceCollection services)
            where TRequest : IRequest<ResultadoOperacao>
            where THandler : class, IContratoBaseHandler<TRequest, ResultadoOperacao>
        {
            services.AddScoped<THandler>();
            services.AddScoped<IContratoBaseHandler<TRequest, ResultadoOperacao>, THandler>();

            return services;
        }
    }
}
