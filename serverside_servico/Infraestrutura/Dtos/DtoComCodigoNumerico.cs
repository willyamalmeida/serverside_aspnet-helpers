using System;

namespace serverside_servico.Infraestrutura.Dtos
{
    [Serializable]
    public abstract class DtoComCodigoNumerico : DtoPadrao
    {
        public int Codigo { get; set; }
    }
}
