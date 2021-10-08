using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Helpers.Builders;

namespace serverside_aspnet.Helpers
{
    public static class HelperTreinamentoLayoutGrid
    {
        public static TreinamentoLayoutGridBuilder TreinamentoLayoutGrid(this IHtmlHelper htmlHelper, bool usaGroupForm = false)
        {
            return new TreinamentoLayoutGridBuilder(htmlHelper, usaGroupForm);
        }
    }
}
