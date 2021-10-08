using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Models.Componentes;

namespace serverside_aspnet.Helpers.Builders
{
    public class ComboboxDepartamentoBuilder : TreinamentoBuilder<TreinamentoCombobox>
    {
        public ComboboxDepartamentoBuilder(IHtmlHelper htmlHelper, string propriedade)
            : base(htmlHelper, propriedade)
        {
            Model.Controller = "Departamento";
            Model.Action = "ConsulteLista";
            Model.CampoChave = "codigo";
            Model.Colunas = new[] { "codigo", "descricao" };
        }

        public ComboboxDepartamentoBuilder Controller(string controller)
        {
            Model.Controller = controller;
            return this;
        }

        public ComboboxDepartamentoBuilder Action(string action)
        {
            Model.Action = action;
            return this;
        }

        protected override string ObtenhaNomeDaView()
        {
            return "_Combobox";
        }
    }
}
