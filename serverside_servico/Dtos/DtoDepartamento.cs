using System;
using serverside_servico.Infraestrutura.Dtos;

namespace serverside_servico.Dtos
{
    [Serializable]
    public class DtoDepartamento : DtoComCodigoNumerico
    {
        public string Descricao { get; set; }
    }
}
