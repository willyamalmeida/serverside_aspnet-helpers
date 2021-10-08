using System;
using serverside_servico.Infraestrutura.Objetos;

namespace serverside_servico.Negocio
{
    [Serializable]
    public class Funcionario : ObjetoComCodigoNumerico
    {
        public virtual string Nome { get; set; }

        public virtual Departamento Departamento { get; set; }
    }
}
