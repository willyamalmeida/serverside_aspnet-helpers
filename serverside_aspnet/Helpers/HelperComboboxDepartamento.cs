using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Helpers.Builders;

namespace serverside_aspnet.Helpers
{
    public static class HelperComboboxDepartamento
    {
        public static ComboboxDepartamentoBuilder ComboboxDepartamento(
            this IHtmlHelper htmlHelper,
            string propriedade)
        {
            return new ComboboxDepartamentoBuilder(htmlHelper, propriedade);
        }

        public static ComboboxDepartamentoBuilder ComboboxDepartamentoFor<TModel, TPropriedade>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TPropriedade>> expression)
        {
            var propriedade = System.Web.Mvc.ExpressionHelper.GetExpressionText(expression);
            return new ComboboxDepartamentoBuilder(htmlHelper, propriedade);
        }
    }
}
