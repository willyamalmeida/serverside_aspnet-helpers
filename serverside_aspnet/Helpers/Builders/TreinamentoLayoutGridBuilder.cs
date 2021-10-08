using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using serverside_aspnet.Models;

namespace serverside_aspnet.Helpers.Builders
{
    public class TreinamentoLayoutGridBuilder : ComponenteBaseBuilder<List<ComponenteLayoutGridModel>>
    {
        private string _usaGroupForm;

        public TreinamentoLayoutGridBuilder(IHtmlHelper htmlHelper, bool usaGroupForm)
            : base(htmlHelper)
        {
            _usaGroupForm = usaGroupForm ? "S" : "N";
        }

        public TreinamentoLayoutGridBuilder AdicioneColuna(
            int coluna,
            Func<object, IHtmlContent> htmlTemplate)
        {
            Model.Add(new ComponenteLayoutGridModel { Coluna = coluna, HtmlTemplate = htmlTemplate });
            return this;
        }

        protected override string ObtenhaNomeDaView()
        {
            return "_LayoutGrid";
        }

        protected override ViewDataDictionary ObtenhaViewData()
        {
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            viewData.Add("UsaGroupForm", _usaGroupForm);

            return viewData;
        }
    }
}
