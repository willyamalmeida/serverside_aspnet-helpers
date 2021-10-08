using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using serverside_aspnet.Models;
using serverside_aspnet.Models.Enumeradores;

namespace serverside_aspnet.Controllers
{
    public class PessoaController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ListaDeEstadoCivil"] = new List<EnumEstadoCivilModel>
            {
                EnumEstadoCivilModel.SOLTEIRO,
                EnumEstadoCivilModel.CASADO,
                EnumEstadoCivilModel.VIUVO
            };

            //return View();

            var model = new PessoaModel
            {
                Contato = new ContatoModel
                {
                    Telefone = "9999999",
                    Celular = "777777",
                    Email = "ddd@ddd.dd.dd"
                }
            };

            return View(model);
        }
    }
}
