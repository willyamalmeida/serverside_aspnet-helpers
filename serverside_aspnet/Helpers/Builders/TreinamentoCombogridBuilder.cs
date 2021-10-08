using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Models;

namespace serverside_aspnet.Helpers.Builders
{
    public abstract class TreinamentoCombogridBuilder<TBuilder> : ComponenteBuilder<TBuilder, ComponenteModel>
        where TBuilder : class
    {
        protected TreinamentoCombogridBuilder(IHtmlHelper htmlHelper, string propriedade)
            : base(htmlHelper, propriedade)
        {
            Model.QtdDeItensRetornados = 7;
        }

        protected TreinamentoCombogridBuilder(IHtmlHelper htmlHelper, Expression<Func<object, object>> propriedade)
            : base(htmlHelper, propriedade)
        {
            Model.QtdDeItensRetornados = 7;
        }

        public TBuilder Action(string action)
        {
            Model.Action = action;
            return this as TBuilder;
        }

        public TBuilder Controller(string controller)
        {
            Model.Controller = controller;
            return this as TBuilder;
        }

        public TBuilder QuantidadeDeItensRetornados(int qtdDeItensRetornados)
        {
            Model.QtdDeItensRetornados = qtdDeItensRetornados;
            return this as TBuilder;
        }

        protected override string ObtenhaNomeDaView()
        {
            return "_Combogrid";
        }
    }
}
