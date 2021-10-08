using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Servicos;

namespace serverside_aspnet.Controllers
{
    public abstract class ControllerPadrao<TDto> : ControllerBase
        where TDto : DtoPadrao, new()
    {
        protected ControllerPadrao(ILogger<ControllerPadrao<TDto>> logger, IServico<TDto> servico)
            : base(logger)
        {
            Logger = logger;
            Servico = servico;
        }

        protected ILogger<ControllerPadrao<TDto>> Logger { get; private set; }

        protected IServico<TDto> Servico { get; private set; }

        public virtual IActionResult Novo()
        {
            var dto = new TDto();
            return View("Editar", dto);
        }
    }
}
