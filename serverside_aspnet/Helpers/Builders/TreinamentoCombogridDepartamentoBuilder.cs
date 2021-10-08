using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace serverside_aspnet.Helpers.Builders
{
    public class TreinamentoCombogridDepartamentoBuilder : TreinamentoCombogridBuilder<TreinamentoCombogridDepartamentoBuilder>
    {
        public TreinamentoCombogridDepartamentoBuilder(IHtmlHelper htmlHelper, string propriedade)
            : base(htmlHelper, propriedade)
        {
            Inicialize();
        }

        public TreinamentoCombogridDepartamentoBuilder(IHtmlHelper htmlHelper, Expression<Func<object, object>> propriedade)
            : base(htmlHelper, propriedade)
        {
            Inicialize();
        }

        private void Inicialize()
        {
            Model.Action = "Consulte";
            Model.Controller = "Departamento";
            Model.Colunas = "[{\"name\": \"codigo\", \"label\": \"Código\"}, {\"name\": \"descricao\", \"label\": \"Descrição\"}]";
            Model.CampoChave = "codigo";
            Model.CamposTemplate = new[] { "codigo", "descricao" };
        }
    }
}
