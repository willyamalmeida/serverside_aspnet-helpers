using Microsoft.AspNetCore.Mvc.Rendering;

namespace serverside_aspnet.Helpers.Builders
{
    public class TreinamentoTituloBuilder : ComponenteBaseBuilder<object>
    {
        public TreinamentoTituloBuilder(IHtmlHelper htmlHelper, string titulo)
            : base(htmlHelper, titulo)
        {
        }

        protected override string ObtenhaNomeDaView()
        {
            return "_Titulo";
        }
    }
}
