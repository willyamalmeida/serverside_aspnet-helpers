using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Servicos;

namespace serverside_aspnet.Controllers
{
    public abstract class ControllerComCodigoNumerico<TDto> : ControllerPadrao<TDto>
        where TDto : DtoComCodigoNumerico, new()
    {
        protected ControllerComCodigoNumerico(ILogger<ControllerComCodigoNumerico<TDto>> logger, IServicoComCodigoNumerico<TDto> servico)
            : base(logger, servico)
        {
        }

        public IServicoComCodigoNumerico<TDto> ServicoComCodigoNumerico
        {
            get
            {
                return (IServicoComCodigoNumerico<TDto>)Servico;
            }
        }

        [HttpPost]
        public virtual IActionResult Salvar(TDto dto)
        {
            var novoRegistro = dto.Codigo == 0;

            if (novoRegistro)
            {
                Servico.Cadastre(dto);
            }
            else
            {
                Servico.Atualize(dto);
            }

            return RedirectToAction("Index");
        }

        public virtual IActionResult Excluir(int codigo)
        {
            ServicoComCodigoNumerico.Exclue(codigo);

            return RedirectToAction("Index");
        }

        public virtual IActionResult Editar(int codigo)
        {
            var dto = ServicoComCodigoNumerico.Consulte(codigo);

            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            var lista = Servico.ConsulteLista();
            return Json(Ordernar(lista));
        }

        [HttpGet]
        public virtual IActionResult Consulte(string filtro, int quantidade)
        {
            filtro = filtro != null ? HtmlEncoder.Default.Encode(filtro) : filtro;
            var resultado = ServicoComCodigoNumerico.Consulte(filtro, quantidade);
            return Json(resultado);
        }

        [HttpGet]
        public virtual IActionResult ConsulteParcial(string filtro, int pagina, int quantidade)
        {
            filtro = filtro != null ? HtmlEncoder.Default.Encode(filtro) : filtro;
            var resultado = ServicoComCodigoNumerico.ConsulteParcial(filtro, pagina, quantidade);
            return Json(resultado);
        }

        [HttpGet]
        public virtual IActionResult ConsultePaginado(string filtro, int pagina, int quantidade)
        {
            filtro = filtro != null ? HtmlEncoder.Default.Encode(filtro) : filtro;
            var resultado = ServicoComCodigoNumerico.ConsultePaginado(filtro, pagina, quantidade);
            return Json(resultado);
        }

        protected virtual List<TDto> Ordernar(List<TDto> lista)
        {
            var listaOrdernada = lista.OrderBy(x => x.Codigo).ToList();
            return listaOrdernada;
        }
    }
}
