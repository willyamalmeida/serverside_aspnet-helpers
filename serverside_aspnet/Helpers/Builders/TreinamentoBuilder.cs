using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Models.Componentes;

namespace serverside_aspnet.Helpers.Builders
{
    public abstract class TreinamentoBuilder<TModel> : IHtmlContent
        where TModel : TreinamentoModel, new()
    {
        protected TreinamentoBuilder(
            IHtmlHelper htmlHelper,
            string propriedade)
        {
            HtmlHelper = htmlHelper;

            Model = new TModel
            {
                Propriedade = propriedade
            };
        }

        protected IHtmlHelper HtmlHelper { get; private set; }

        public TModel Model { get; private set; }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            var nomeDaView = ObtenhaNomeDaView();

            HtmlHelper.RenderPartialAsync(nomeDaView, Model, null);
        }

        protected abstract string ObtenhaNomeDaView();
    }
}
