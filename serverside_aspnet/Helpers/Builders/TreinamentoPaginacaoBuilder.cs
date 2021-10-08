using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Models;

namespace serverside_aspnet.Helpers.Builders
{
    public class TreinamentoPaginacaoBuilder : ComponenteBuilder<TreinamentoPaginacaoBuilder, ComponentePaginacaoModel>
    {
        public TreinamentoPaginacaoBuilder(IHtmlHelper htmlHelper, string propriedade)
            : base(htmlHelper, propriedade)
        {
            Model.Quantidade = 0;
        }

        public TreinamentoPaginacaoBuilder(IHtmlHelper htmlHelper, Expression<Func<object, object>> propriedade)
            : base(htmlHelper, propriedade)
        {
            Model.Quantidade = 0;
        }

        public TreinamentoPaginacaoBuilder Action(string action)
        {
            Model.Action = action;
            return this;
        }

        public TreinamentoPaginacaoBuilder Controller(string controller)
        {
            Model.Controller = controller;
            return this;
        }

        public TreinamentoPaginacaoBuilder Quantidade(int quantidade)
        {
            Model.Quantidade = quantidade;
            return this;
        }

        public TreinamentoPaginacaoBuilder Pagina(int pagina)
        {
            Model.Pagina = pagina;
            return this;
        }

        public TreinamentoPaginacaoBuilder ApiTabela(string apiTabela)
        {
            Model.ApiTabela = apiTabela;
            return this;
        }
        public TreinamentoPaginacaoBuilder SeletorTabela(string seletorTabela)
        {
            Model.SeletorTabela = seletorTabela;
            return this;
        }

        protected override string ObtenhaNomeDaView()
        {
            return "_Paginacao";
        }
    }
}
