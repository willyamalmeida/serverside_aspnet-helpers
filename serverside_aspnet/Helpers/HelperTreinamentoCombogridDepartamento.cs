using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Helpers.Builders;

namespace serverside_aspnet.Helpers
{
    public static class HelperTreinamentoCombogridDepartamento
    {
        public static TreinamentoCombogridDepartamentoBuilder TreinamentoCombogridDepartamento<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            string nome)
        {
            return new TreinamentoCombogridDepartamentoBuilder(htmlHelper, nome);
        }

        public static TreinamentoCombogridDepartamentoBuilder TreinamentoCombogridDepartamentoFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var nome = System.Web.Mvc.ExpressionHelper.GetExpressionText(expression);
            return new TreinamentoCombogridDepartamentoBuilder(htmlHelper, nome);
        }
    }
}
