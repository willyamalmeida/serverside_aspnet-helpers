using System;
using serverside_servico.Infraestrutura.Dtos;

namespace serverside_servico.Dtos
{
    [Serializable]
    public class DtoFuncionario : DtoComCodigoNumerico
    {
        public string Nome { get; set; }

        public DtoDepartamento Departamento { get; set; }
    }
}
