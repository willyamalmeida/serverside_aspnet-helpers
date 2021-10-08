using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Models;

namespace serverside_aspnet.Helpers.Builders
{
    public class TreinamentoTextBoxBuilder : ComponenteBuilder<TreinamentoTextBoxBuilder, ComponenteTextModel>
    {
        public TreinamentoTextBoxBuilder(IHtmlHelper htmlHelper, string propriedade)
            : base(htmlHelper, propriedade)
        {
        }

        public TreinamentoTextBoxBuilder(IHtmlHelper htmlHelper, string propriedade, bool obrigatorio = false, bool desabilite = false)
            : base(htmlHelper, propriedade)
        {
            if (obrigatorio)
            {
                Model.Placeholder = "Campo obrigatório";
            }

            Model.Desabilite = desabilite;
        }

        public TreinamentoTextBoxBuilder(IHtmlHelper htmlHelper, Expression<Func<object, object>> propriedade)
            : base(htmlHelper, propriedade)
        {
        }

        public TreinamentoTextBoxBuilder AdicionePlaceholder(string placeholder)
        {
            Model.Placeholder = placeholder;
            return this;
        }

        protected override string ObtenhaNomeDaView()
        {
            return "_TextBox";
        }
    }
}
