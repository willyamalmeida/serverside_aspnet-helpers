using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using serverside_servico.Dtos;
using serverside_servico.Interfaces.Servicos;

namespace serverside_aspnet.Controllers
{
    public class DepartamentoController : ControllerComCodigoNumerico<DtoDepartamento>
    {
        public DepartamentoController(ILogger<DepartamentoController> logger, IServicoDeDepartamento servico)
            : base(logger, servico)
        {
        }

        public IActionResult ConsulteLista()
        {
            var lista = Servico.ConsulteLista();
            lista = Ordernar(lista);

            return Json(lista);
        }
    }
}
