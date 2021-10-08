using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Helpers.Builders;

namespace serverside_aspnet.Helpers
{
    public static class HelperTreinamentoLabel
    {
        public static TreinamentoLabelBuilder TreinamentoLabel<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            string nome)
        {
            return new TreinamentoLabelBuilder(htmlHelper, nome, nome);
        }

        public static TreinamentoLabelBuilder TreinamentoLabel<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            string nome,
            string label)
        {
            return new TreinamentoLabelBuilder(htmlHelper, nome, label);
        }

        public static TreinamentoLabelBuilder TreinamentoLabelFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            bool obrigatorio = false)
        {
            var nome = System.Web.Mvc.ExpressionHelper.GetExpressionText(expression);
            return new TreinamentoLabelBuilder(htmlHelper, nome, nome, obrigatorio);
        }

        public static TreinamentoLabelBuilder TreinamentoLabelFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string label,
            bool obrigatorio = false)
        {
            var nome = System.Web.Mvc.ExpressionHelper.GetExpressionText(expression);
            return new TreinamentoLabelBuilder(htmlHelper, nome, label, obrigatorio);
        }
    }
}
