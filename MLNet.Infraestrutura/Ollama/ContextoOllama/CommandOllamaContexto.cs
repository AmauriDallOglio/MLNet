using Microsoft.EntityFrameworkCore;
using MLNet.Infraestrutura.MLNet.Contexto;

namespace MLNet.Infraestrutura.Ollama.ContextoOllama
{
    public class CommandOllamaContexto : GenericoContexto
    {
        public CommandOllamaContexto(DbContextOptions<CommandOllamaContexto> options) : base(options)
        {

        }
    }
}
