using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Helpers.Builders;

namespace serverside_aspnet.Helpers
{
    public static class HelperTreinamentoTabela
    {
        public static TreinamentoTabelaBuilder TreinamentoTabela(this IHtmlHelper htmlHelper, string propriedade)
        {
            return new TreinamentoTabelaBuilder(htmlHelper, propriedade);
        }
    }
}
