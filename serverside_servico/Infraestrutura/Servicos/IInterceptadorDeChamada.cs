using System;

namespace serverside_servico.Infraestrutura.Servicos
{
    public interface IInterceptadorDeChamada
    {
        void Execute(Action escopo);
    }
}
