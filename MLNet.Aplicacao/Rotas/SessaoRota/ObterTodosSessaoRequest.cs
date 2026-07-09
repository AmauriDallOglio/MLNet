using MLNet.Aplicacao.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLNet.Aplicacao.Rotas.SessaoRota
{
    public class ObterTodosSessaoRequest : IRequest<ResultadoOperacao>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
