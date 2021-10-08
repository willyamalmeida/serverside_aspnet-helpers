using System;
using System.Data;
using serverside_servico.Infraestrutura.Persistencias;

namespace serverside_servico.Infraestrutura.Servicos
{
    public class ControladorDeTransacao : IInterceptadorDeChamada
    {
        public void Execute(Action escopo)
        {
            using (var transacao = UtilitarioNHibernate.Sessao.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                UtilitarioNHibernate.Sessao.Clear();
                escopo();
                transacao.Commit();
            }
        }
    }
}
