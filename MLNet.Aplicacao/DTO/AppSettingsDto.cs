namespace MLNet.Aplicacao.DTO
{
    public class AppSettingsDto
    {
        public LoggingDto Logging { get; set; } = new LoggingDto();

        public ConnectionStringsDto ConnectionStrings { get; set; } = new ConnectionStringsDto();

    }

    public class ConnectionStringsDto
    {
        public string ConexaoServidor { get; set; } = string.Empty;
        public string ConexaoServidorQuery { get; set; } = string.Empty;
    }

    public class LoggingDto
    {
        public LogLevelDto LogLevel { get; set; } = new LogLevelDto();
    }

    public class LogLevelDto
    {
        public string Default { get; set; } = string.Empty;
        [System.Text.Json.Serialization.JsonPropertyName("Microsoft.AspNetCore")]
        public string MicrosoftAspNetCore { get; set; } = string.Empty;
    }
}
