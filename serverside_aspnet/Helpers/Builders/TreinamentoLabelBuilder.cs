using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Models;

namespace serverside_aspnet.Helpers.Builders
{
    public class TreinamentoLabelBuilder : ComponenteBuilder<TreinamentoLabelBuilder, ComponenteLabelModel>
    {
        public TreinamentoLabelBuilder(IHtmlHelper htmlHelper, string propriedade, string label, bool obrigatorio = false)
            : base(htmlHelper, propriedade)
        {
            Model.Label = label;
            Model.Obrigatorio = obrigatorio;
        }

        public TreinamentoLabelBuilder(IHtmlHelper htmlHelper, Expression<Func<object, object>> propriedade, string label, bool obrigatorio = false)
            : base(htmlHelper, propriedade)
        {
            Model.Label = label;
            Model.Obrigatorio = obrigatorio;
        }

        protected override string ObtenhaNomeDaView()
        {
            return "_Label";
        }
    }
}
