using Microsoft.Extensions.Logging;

namespace MLNet.Aplicacao.Util
{
    public class PrintaConsole<T> : IPrintaConsole<T>
    {
        private readonly ILogger<T> _logger;


        public PrintaConsole(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Error(string mensagem)
        {
            Padrao(mensagem, ConsoleColor.White, ConsoleColor.Red, LogLevel.Error, "error");
        }

        public void Sucesso(string mensagem)
        {
            Padrao(mensagem, ConsoleColor.Black, ConsoleColor.Green, LogLevel.Information, "success");
        }

        public void Alerta(string mensagem)
        {
            Padrao(mensagem, ConsoleColor.Black, ConsoleColor.Yellow, LogLevel.Warning, "warning");
        }

        public void Info(string mensagem)
        {
            Padrao(mensagem, ConsoleColor.Yellow, ConsoleColor.Blue, LogLevel.Information, "info");
        }

        public void ImprimirSemCor(string mensagem)
        {

            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {mensagem}");
        }

        private void Padrao(string mensagem, ConsoleColor fg, ConsoleColor bg, LogLevel level, string nivel)
        {
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {mensagem}");
            Console.ResetColor();

            _logger.Log(level, mensagem);
      
        }
    }
}
