using System.Diagnostics;
using NHibernate;
using NHibernate.SqlCommand;

namespace serverside_servico.Infraestrutura.Persistencias
{
    public class InterceptadorNHibernate : EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
#if DEBUG
            Trace.WriteLine(sql.ToString());
            return sql;
#endif
        }
    }
}
