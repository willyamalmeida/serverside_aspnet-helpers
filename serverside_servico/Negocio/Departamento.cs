using System;
using serverside_servico.Infraestrutura.Objetos;

namespace serverside_servico.Negocio
{
    [Serializable]
    public class Departamento : ObjetoComCodigoNumerico
    {
        public virtual string Descricao { get; set; }
    }
}
