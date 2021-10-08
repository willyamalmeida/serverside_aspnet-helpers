using serverside_servico.Infraestrutura.Mapeamentos;
using serverside_servico.Negocio;

namespace serverside_servico.Mapeamentos
{
    public class DepartamentoMap : ObjetoComCodigoNumericoMap<Departamento>
    {
        public DepartamentoMap()
        {
            Table("DEPARTAMENTO");

            Map(departamento => departamento.Descricao, "DESCRICAO").Length(100).Not.Nullable();
        }
    }
}
