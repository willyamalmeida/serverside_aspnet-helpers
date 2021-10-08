using FluentNHibernate.Mapping;
using serverside_servico.Infraestrutura.Objetos;

namespace serverside_servico.Infraestrutura.Mapeamentos
{
    public abstract class ObjetoCompartilhadoMap<TObjeto> : ClassMap<TObjeto> where TObjeto : ObjetoCompartilhado
    {
        protected ObjetoCompartilhadoMap()
        {
            Id(objeto => objeto.Id, "ID").GeneratedBy.Guid();
        }
    }
}
