using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using serverside_servico.Infraestrutura.Objetos;

namespace serverside_servico.Infraestrutura.Persistencias
{
    public class Persistencia<TObjeto> : IPersistencia<TObjeto> where TObjeto : ObjetoCompartilhado
    {
        public ISession Sessao
        {
            get
            {
                return UtilitarioNHibernate.Sessao;
            }
        }

        public Type ElementType => Query().ElementType;

        public Expression Expression => Query().Expression;

        public IQueryProvider Provider => Query().Provider;

        public IEnumerator<TObjeto> GetEnumerator()
        {
            return Query().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            UtilitarioNHibernate.FinalizeSessao();
        }

        public IQueryable<TObjeto> Query(Expression<Func<TObjeto, bool>> filtroAdicional = null)
        {
            var query = filtroAdicional == null
                ? Sessao.Query<TObjeto>()
                : Sessao.Query<TObjeto>().Where(filtroAdicional);

            return query;
        }

        public void Cadastre(TObjeto objeto)
        {
            Sessao.SaveOrUpdate(objeto);
            Sessao.Flush();
        }

        public void Atualize(TObjeto objeto)
        {
            Sessao.Update(objeto);
            Sessao.Flush();
        }

        public void Exclue(TObjeto objeto)
        {
            Sessao.Delete(objeto);
            Sessao.Flush();
        }

        public TObjeto Consulte(Guid id)
        {
            var resultadoBuscaNaSessao = Sessao.Get<TObjeto>(id);
            var resultado = resultadoBuscaNaSessao ?? this.FirstOrDefault(objeto => objeto.Id == id);

            return resultado;
        }

        public List<TObjeto> Consulte(Expression<Func<TObjeto, bool>> expressao)
        {
            var listaDeObjetos = Query(expressao).ToList();
            return listaDeObjetos;
        }

        public List<TObjeto> Consulte(string filtro, int quantidade, Func<string, Expression<Func<TObjeto, bool>>> obtenhaFiltro)
        {
            IQueryable<TObjeto> queryComFiltro = null;

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.ToUpperInvariant();
                var expressao = obtenhaFiltro(filtro);

                queryComFiltro = expressao == null ? Query() : Query().Where(expressao);
            }

            var query = queryComFiltro == null
                ? Query()
                : queryComFiltro;

            var resultado = query.Take(quantidade).ToList();

            return resultado;
        }

        public List<TObjeto> ConsulteLista()
        {
            var listaDeObjetos = Query().ToList();
            return listaDeObjetos;
        }

        public bool ExisteRegistro(Guid id)
        {
            var resultado = Query(objeto => objeto.Id == id).Select(objeto => objeto.Id).FirstOrDefault();
            var existeRegistro = resultado != Guid.Empty;

            return existeRegistro;
        }

        public ISQLQuery CreateSQLQuery(string sql)
        {
            return Sessao.CreateSQLQuery(sql);
        }
    }
}
