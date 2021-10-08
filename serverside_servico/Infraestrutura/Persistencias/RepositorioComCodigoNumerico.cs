using System;
using System.Linq;
using System.Linq.Expressions;
using serverside_servico.Infraestrutura.Objetos;
using serverside_servico.Infraestrutura.Repositorios;

namespace serverside_servico.Infraestrutura.Persistencias
{
    public abstract class RepositorioComCodigoNumerico<TObjeto> : Repositorio<TObjeto>, IRepositorioComCodigoNumerico<TObjeto> where TObjeto : ObjetoComCodigoNumerico
    {
        public override void Cadastre(TObjeto objeto)
        {
            objeto.Codigo = ObtenhaProximoCodigo();
            base.Cadastre(objeto);
        }

        public TObjeto Consulte(int codigo)
        {
            var query = Persistencia.Where(x => x.Codigo == codigo);
            var objeto = query.FirstOrDefault();

            return objeto;
        }

        public ObjetoPaginado<TObjeto> ConsulteParcial(string filtro, int pagina, int quantidade, Func<string, Expression<Func<TObjeto, bool>>> obtenhaFiltro)
        {
            return ConsultePaginada(filtro, pagina, quantidade, obtenhaFiltro);
        }

        public ObjetoPaginado<TObjeto> ConsultePaginada(string filtro, int pagina, int quantidade, Func<string, Expression<Func<TObjeto, bool>>> obtenhaFiltro)
        {
            var objetoPaginado = new ObjetoPaginado<TObjeto>
            {
                Pagina = pagina,
                Quantidade = quantidade
            };

            IQueryable<TObjeto> query = Persistencia;

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.ToUpperInvariant();
                var expressao = obtenhaFiltro(filtro);

                query = expressao == null ? query : query.Where(expressao);
            }

            objetoPaginado.TotalDeItens = query.Count();
            objetoPaginado.Lista = query
               .OrderBy(x => x.Codigo)
               .Skip((pagina - 1) * quantidade)
               .Take(quantidade)
               .ToList();

            //TObjeto objetoAlias = null;
            //objetoPaginado.Lista = UtilitarioNHibernate.Sessao.QueryOver<TObjeto>(() => objetoAlias)
            //    //.Where(() => objetoAlias.Codigo == 1)
            //    .OrderBy(x => x.Codigo).Asc
            //    .Skip((pagina - 1) * quantidade)
            //    .Take(quantidade)
            //    .List()
            //    .ToList();

            return objetoPaginado;
        }

        private int ObtenhaProximoCodigo()
        {
            var entidade = ObtenhaEntidade();
            var tabela = entidade.TableName.ToUpperInvariant();

            var sql =
                " WITH CODIGOS AS (" +
                " SELECT" +
                "   CODIGO AS ATUAL" +
                " , LEAD(CODIGO) OVER(ORDER BY CODIGO) AS PROXIMO" +
                "   FROM " + tabela +
                " )" +
                " SELECT ATUAL + 1" +
                " FROM CODIGOS" +
                " WHERE PROXIMO - ATUAL > 1";

            var query = Persistencia.CreateSQLQuery(sql);
            var resultado = query.UniqueResult();

            if (resultado != null)
            {
                return Convert.ToInt32(resultado);
            }

            var maiorCodigo = Persistencia.Max(x => x.Codigo);
            var proximoCodigo = maiorCodigo + 1;

            return proximoCodigo;
        }
    }
}
