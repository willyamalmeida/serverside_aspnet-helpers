using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Helpers.Builders;

namespace serverside_aspnet.Helpers
{
    public static class HelperTreinamentoTitulo
    {
        public static TreinamentoTituloBuilder TreinamentoTitulo(this IHtmlHelper htmlHelper, string titulo)
        {
            return new TreinamentoTituloBuilder(htmlHelper, titulo);
        }
    }
}
