using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate.Persister.Entity;
using serverside_servico.Infraestrutura.Objetos;
using serverside_servico.Infraestrutura.Repositorios;

namespace serverside_servico.Infraestrutura.Persistencias
{
    public abstract class Repositorio<TObjeto> : IRepositorio<TObjeto>, IDisposable where TObjeto : ObjetoCompartilhado
    {
        private IPersistencia<TObjeto> _persistencia;

        protected IPersistencia<TObjeto> Persistencia
        {
            get
            {
                return _persistencia ??= new Persistencia<TObjeto>();
            }
        }

        public virtual void Cadastre(TObjeto objeto)
        {
            Persistencia.Cadastre(objeto);
        }

        public virtual void Atualize(TObjeto objeto)
        {
            Persistencia.Atualize(objeto);
        }

        public virtual void Exclue(TObjeto objeto)
        {
            Persistencia.Exclue(objeto);
        }

        public TObjeto Consulte(Guid id)
        {
            return Persistencia.Consulte(id);
        }

        public List<TObjeto> Consulte(Expression<Func<TObjeto, bool>> expressao)
        {
            return Persistencia.Consulte(expressao);
        }

        public List<TObjeto> Consulte(string filtro, int quantidade, Func<string, Expression<Func<TObjeto, bool>>> obtenhaFiltro)
        {
            return Persistencia.Consulte(filtro, quantidade, obtenhaFiltro);
        }

        public List<TObjeto> ConsulteLista()
        {
            return Persistencia.ConsulteLista();
        }

        public bool ExisteRegistro(Guid id)
        {
            return Persistencia.ExisteRegistro(id);
        }

        public void Dispose()
        {
            Persistencia.Dispose();
        }

        protected AbstractEntityPersister ObtenhaEntidade()
        {
            var entidade = UtilitarioNHibernate
                .Sessao
                .SessionFactory
                .GetClassMetadata(typeof(TObjeto)) as AbstractEntityPersister;

            return entidade;
        }
    }
}
