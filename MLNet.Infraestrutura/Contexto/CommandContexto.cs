using Microsoft.EntityFrameworkCore;

namespace MLNet.Infraestrutura.Contexto
{
    public class CommandContexto : GenericoContexto
    {
        public CommandContexto(DbContextOptions<CommandContexto> options) : base(options)
        {

        }
    }
}
