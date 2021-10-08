using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Helpers.Builders;

namespace serverside_aspnet.Helpers
{
    public static class HelperTreinamentoTextBox
    {
        public static TreinamentoTextBoxBuilder TreinamentoTextBox<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            string nome)
        {
            return new TreinamentoTextBoxBuilder(htmlHelper, nome);
        }

        public static TreinamentoTextBoxBuilder TreinamentoTextBoxFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var nome = System.Web.Mvc.ExpressionHelper.GetExpressionText(expression);
            return new TreinamentoTextBoxBuilder(htmlHelper, nome);
        }

        public static TreinamentoTextBoxBuilder TreinamentoTextBoxFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            bool obrigatorio = false,
            bool desabilite = false)
        {
            var nome = System.Web.Mvc.ExpressionHelper.GetExpressionText(expression);
            return new TreinamentoTextBoxBuilder(htmlHelper, nome, obrigatorio, desabilite);
        }
    }
}
