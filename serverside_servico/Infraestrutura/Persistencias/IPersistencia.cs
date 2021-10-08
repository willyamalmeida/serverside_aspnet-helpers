using System;
using System.Linq;
using NHibernate;
using serverside_servico.Infraestrutura.Objetos;
using serverside_servico.Infraestrutura.Repositorios;

namespace serverside_servico.Infraestrutura.Persistencias
{
    public interface IPersistencia<TObjeto> : IQueryable<TObjeto>, IDisposable, IRepositorio<TObjeto> where TObjeto : ObjetoCompartilhado
    {
        ISQLQuery CreateSQLQuery(string sql);
    }
}
