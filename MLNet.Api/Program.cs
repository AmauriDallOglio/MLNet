using MLNet.Api.Configuracao;
using MLNet.Api.Middleware;
using MLNet.Aplicacao.DTO;

var builder = WebApplication.CreateBuilder(args);

string environmentName = builder.Environment.EnvironmentName;
IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();



builder.Services.Configure<ApiKeyConfiguration>(configuration.GetSection("Security"));
AppSettingsConfiguracao.Carregar(builder.Services, configuration);

InjecaoDependenciaConfiguracao.RegistrarServicos(builder);
builder.Services.RegistrarCqrs();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "X-Api-Key",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "Informe a API Key do serviço"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }
    await next();
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.UseMiddleware<ApiKeyMiddleware>();

var appSettings = app.Services.GetRequiredService<AppSettingsDto>();
if (appSettings.RateLimit.Habilitado)
    app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
