using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Web.WebPages;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using serverside_aspnet.Models;

namespace serverside_aspnet.Helpers.Builders
{
    public abstract class ComponenteBuilder<TBuilder, TModel> : IHtmlContent
        where TBuilder : class
        where TModel : ComponentePadraoModel, new()
    {
        protected ComponenteBuilder(IHtmlHelper htmlHelper, string propriedade)
        {
            HtmlHelper = htmlHelper;
            Model = new TModel
            {
                Propriedade = propriedade,
                IdentificadorDaPropriedade = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(propriedade),
                HtmlAttributes = new Dictionary<string, object>()
            };
        }

        protected ComponenteBuilder(IHtmlHelper htmlHelper, Expression<Func<object, object>> propriedade)
            : this(htmlHelper, System.Web.Mvc.ExpressionHelper.GetExpressionText(propriedade))
        {
        }

        protected IHtmlHelper HtmlHelper { get; private set; }

        protected TModel Model { get; private set; }

        public TBuilder Obrigatorio(bool obrigatorio)
        {
            Model.Obrigatorio = obrigatorio;
            return this as TBuilder;
        }

        public TBuilder HtmlAttributes(object atributosHtml)
        {
            Model.HtmlAttributes = new RouteValueDictionary(atributosHtml);
            return this as TBuilder;
        }

        public TBuilder HtmlAttributes(IDictionary<string, object> dicionarioDeAtributos)
        {
            Model.HtmlAttributes = new RouteValueDictionary(dicionarioDeAtributos);
            return this as TBuilder;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            var nomeDaView = ObtenhaNomeDaView();

            PreparaModel();

            HtmlHelper.RenderPartialAsync(nomeDaView, Model, null);
        }

        protected virtual void PreparaModel()
        {
            if (Model.Obrigatorio)
            {
                if (!Model.HtmlAttributes.ContainsKey("class"))
                {
                    Model.HtmlAttributes.Add("class", "obrigatorio");
                }
                else
                {
                    var classe = Model.HtmlAttributes["class"] as string;
                    classe += " obrigatorio";

                    Model.HtmlAttributes["class"] = classe;
                }
            }

            if (Model.Objeto == null)
            {
                var modelViewData = HtmlHelper.ViewData.Model;
                Model.Objeto = ObtenhaValorDaPropriedade(modelViewData, Model.Propriedade);
            }
        }

        protected abstract string ObtenhaNomeDaView();

        private object ObtenhaValorDaPropriedade(object objeto, string nomeDaPropriedade)
        {
            object retorno = null;

            if (objeto != null && nomeDaPropriedade != null)
            {
                // Cobre expressions do tipo x => x.y.z
                if (nomeDaPropriedade.Contains("."))
                {
                    var fimDaPropriedade = nomeDaPropriedade.IndexOf('.');
                    var nomeEspecificoDaPropriedade = nomeDaPropriedade.Substring(0, fimDaPropriedade);
                    var nomeRealDaPropriedade = ObtenhaNomeReal(nomeEspecificoDaPropriedade);
                    var propriedade = objeto.GetType().GetProperty(nomeRealDaPropriedade);

                    if (propriedade != null)
                    {
                        var valor = propriedade.GetValue(objeto, null);

                        if (valor is IEnumerable)
                        {
                            valor = ObtenhaValorDaLista(nomeEspecificoDaPropriedade, valor);
                        }

                        nomeDaPropriedade = nomeDaPropriedade.Substring(fimDaPropriedade + 1);
                        retorno = ObtenhaValorDaPropriedade(valor, nomeDaPropriedade);
                    }
                }
                else
                {
                    var nomeRealDaPropriedade = ObtenhaNomeReal(nomeDaPropriedade);
                    var prop = objeto.GetType().GetProperty(nomeRealDaPropriedade);

                    // Por Ordem:
                    // 1. Cobre expressions do tipo x => x
                    // 2. Cobre expressions do tipo x => x[n]
                    // 3. Outras expressions
                    if (nomeDaPropriedade.IsEmpty())
                    {
                        retorno = objeto;
                    }
                    else if (new Regex(@"^\[\d+\]$").IsMatch(nomeDaPropriedade))
                    {
                        retorno = ObtenhaValorDaLista(nomeDaPropriedade, objeto);
                    }
                    else if (prop != null)
                    {
                        var valor = prop.GetValue(objeto, null);

                        if (nomeDaPropriedade.Contains("[") && valor.GetType() != typeof(string) && valor is IEnumerable)
                        {
                            valor = ObtenhaValorDaLista(nomeDaPropriedade, valor);
                        }

                        retorno = valor;
                    }
                }
            }

            return retorno;
        }

        private object ObtenhaValorDaLista(string nomeDaPropriedade, object objeto)
        {
            object retorno = null;
            var lista = objeto as IList;

            if (lista != null && lista.Count > 0)
            {
                var indice = ObtenhaIndice(nomeDaPropriedade);

                if (indice >= 0)
                {
                    retorno = lista[indice];
                }
            }

            return retorno;
        }

        private int ObtenhaIndice(string nomeDaPropriedade)
        {
            return int.Parse(
                             Regex.Match(nomeDaPropriedade, "\\[\\d+\\]").ToString().Replace("[", string.Empty).Replace("]", string.Empty),
                             CultureInfo.InvariantCulture);
        }

        private string ObtenhaNomeReal(string nomePropriedade)
        {
            var nomeReal = nomePropriedade;

            if (nomeReal.Contains("["))
            {
                var indice = nomeReal.IndexOf('[');
                nomeReal = nomeReal.Substring(0, indice);
            }

            return nomeReal;
        }
    }
}
