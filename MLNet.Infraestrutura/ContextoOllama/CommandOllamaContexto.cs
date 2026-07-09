using Microsoft.EntityFrameworkCore;
using MLNet.Infraestrutura.Contexto;

namespace MLNet.Infraestrutura.ContextoOllama
{
    public class CommandOllamaContexto : GenericoContexto
    {
        public CommandOllamaContexto(DbContextOptions<CommandOllamaContexto> options) : base(options)
        {

        }
    }
}
