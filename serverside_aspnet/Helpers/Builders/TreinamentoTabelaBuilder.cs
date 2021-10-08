using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using serverside_aspnet.Models;
using serverside_aspnet.Models.Componentes;

namespace serverside_aspnet.Helpers.Builders
{
    public class TreinamentoTabelaBuilder : ComponenteBaseBuilder<ComponenteTabela>
    {
        public TreinamentoTabelaBuilder(IHtmlHelper htmlHelper, string propriedade)
            : base(htmlHelper)
        {
            Model.Propriedade = propriedade;
            Model.IdentificadorDaPropriedade = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(propriedade);
        }

        public TreinamentoTabelaBuilder(
            IHtmlHelper htmlHelper,
            string propriedade,
            string controller,
            string actionConsulte,
            string actionEditar,
            int pagina,
            int quantidade,
            bool habiliteFiltroDePesquisa,
            List<Coluna> colunas)
            : base(htmlHelper)
        {
            Model.Propriedade = propriedade;
            Model.IdentificadorDaPropriedade = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(propriedade);
            Model.Controller = controller;
            Model.ActionConsulte = actionConsulte;
            Model.ActionEditar = actionEditar;
            Model.Colunas = colunas;
            Model.Pagina = pagina;
            Model.Quantidade = quantidade;
            Model.HabiliteFiltroDePesquisa = habiliteFiltroDePesquisa;
        }

        public TreinamentoTabelaBuilder Controller(string controller)
        {
            Model.Controller = controller;
            return this;
        }

        public TreinamentoTabelaBuilder ActionConsulte(string action)
        {
            Model.ActionConsulte = action;
            return this;
        }

        public TreinamentoTabelaBuilder ActionEditar(string action)
        {
            Model.ActionEditar = action;
            return this;
        }

        public TreinamentoTabelaBuilder Paginacao(int pagina, int quantidade)
        {
            Model.Pagina = pagina;
            Model.Quantidade = quantidade;

            return this;
        }

        public TreinamentoTabelaBuilder Colunas(List<Coluna> colunas)
        {
            Model.Colunas = colunas;
            return this;
        }

        public TreinamentoTabelaBuilder AdicioneColuna(
            string label,
            string data,
            int tamanho = 0,
            bool ehCampoChave = false)
        {
            Model.Colunas.Add(new Coluna
            {
                Label = label,
                Data = data,
                Tamanho = tamanho,
                EhCampoChave = ehCampoChave
            });

            return this;
        }

        public TreinamentoTabelaBuilder HabiliteFiltro()
        {
            Model.HabiliteFiltroDePesquisa = true;
            return this;
        }

        protected override string ObtenhaNomeDaView()
        {
            return "_Tabela";
        }
    }
}
