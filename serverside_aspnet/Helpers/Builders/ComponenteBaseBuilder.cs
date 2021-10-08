using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace serverside_aspnet.Helpers.Builders
{
    public abstract class ComponenteBaseBuilder<TModel> : IHtmlContent
        where TModel : new()
    {
        protected ComponenteBaseBuilder(IHtmlHelper htmlHelper)
            : this(htmlHelper, new TModel())
        {
        }

        protected ComponenteBaseBuilder(IHtmlHelper htmlHelper, TModel model)
        {
            HtmlHelper = htmlHelper;
            Model = model;
        }

        protected IHtmlHelper HtmlHelper { get; private set; }

        protected TModel Model { get; private set; }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            var nomeDaView = ObtenhaNomeDaView();
            var viewData = ObtenhaViewData();

            PreparaModel();

            HtmlHelper.RenderPartialAsync(nomeDaView, Model, viewData);
        }

        protected virtual void PreparaModel()
        {
        }

        protected virtual ViewDataDictionary ObtenhaViewData()
        {
            return null;
        }

        protected abstract string ObtenhaNomeDaView();
    }
}
