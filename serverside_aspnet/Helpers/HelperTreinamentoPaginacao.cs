using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Helpers.Builders;

namespace serverside_aspnet.Helpers
{
    public static class HelperTreinamentoPaginacao
    {
        public static TreinamentoPaginacaoBuilder TreinamentoPaginacao<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            string nome)
        {
            return new TreinamentoPaginacaoBuilder(htmlHelper, nome);
        }

        public static TreinamentoPaginacaoBuilder TreinamentoPaginacaoFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var nome = System.Web.Mvc.ExpressionHelper.GetExpressionText(expression);
            return new TreinamentoPaginacaoBuilder(htmlHelper, nome);
        }
    }
}
