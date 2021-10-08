using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Models;

namespace serverside_aspnet.Helpers.Builders
{
    public class TreinamentoBarraDeBotoesBuilder : ComponenteBaseBuilder<List<ComponenteBarraDeBotoesModel>>
    {
        public TreinamentoBarraDeBotoesBuilder(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public TreinamentoBarraDeBotoesBuilder Adicione(string nome)
        {
            AdicioneMenu(nome, "#", new { }, true);
            return this;
        }

        public TreinamentoBarraDeBotoesBuilder Adicione(string nome, string action, dynamic parametros, bool adicione = true)
        {
            AdicioneMenu(nome, action, parametros, adicione);
            return this;
        }

        public TreinamentoBarraDeBotoesBuilder Adicione(string nome, string action)
        {
            AdicioneMenu(nome, action, new { }, true);
            return this;
        }

        protected override string ObtenhaNomeDaView()
        {
            return "_BarraDeBotoes";
        }

        private void AdicioneMenu(string nome, string action, dynamic parametros, bool adicione)
        {
            if (adicione)
            {
                Model.Add(new ComponenteBarraDeBotoesModel
                {
                    Nome = nome,
                    Action = action,
                    Parametros = parametros
                });
            }
        }
    }
}
