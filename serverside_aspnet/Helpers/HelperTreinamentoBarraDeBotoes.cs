using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Helpers.Builders;

namespace serverside_aspnet.Helpers
{
    public static class HelperTreinamentoBarraDeBotoes
    {
        public static TreinamentoBarraDeBotoesBuilder TreinamentoBarraDeBotoes(this IHtmlHelper htmlHelper)
        {
            return new TreinamentoBarraDeBotoesBuilder(htmlHelper);
        }
    }
}
