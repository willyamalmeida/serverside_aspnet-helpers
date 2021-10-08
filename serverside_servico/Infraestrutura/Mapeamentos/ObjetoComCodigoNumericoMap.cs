using serverside_servico.Infraestrutura.Objetos;

namespace serverside_servico.Infraestrutura.Mapeamentos
{
    public abstract class ObjetoComCodigoNumericoMap<TObjeto> : ObjetoCompartilhadoMap<TObjeto> where TObjeto : ObjetoComCodigoNumerico
    {
        protected ObjetoComCodigoNumericoMap()
        {
            Map(objeto => objeto.Codigo, "CODIGO");
        }
    }
}
