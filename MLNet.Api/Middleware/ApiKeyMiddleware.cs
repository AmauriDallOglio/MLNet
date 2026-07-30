namespace MLNet.Api.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var expectedApiKey = _configuration["Security:ApiKey"];
            var providedApiKey = context.Request.Headers["X-Api-Key"].ToString();

            if (string.IsNullOrWhiteSpace(expectedApiKey) || string.IsNullOrWhiteSpace(providedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { sucesso = false, mensagem = "API Key ausente." });
                return;
            }

            if (!string.Equals(providedApiKey, expectedApiKey, StringComparison.Ordinal))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { sucesso = false, mensagem = "API Key inválida." });
                return;
            }

            await _next(context);
        }
    }
}
