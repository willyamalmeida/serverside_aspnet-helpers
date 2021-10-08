using Microsoft.Extensions.Logging;
using serverside_servico.Dtos;
using serverside_servico.Interfaces.Servicos;

namespace serverside_aspnet.Controllers
{
    public class FuncionarioController : ControllerComCodigoNumerico<DtoFuncionario>
    {
        public FuncionarioController(ILogger<FuncionarioController> logger, IServicoDeFuncionario servico)
            : base(logger, servico)
        {
        }
    }
}
