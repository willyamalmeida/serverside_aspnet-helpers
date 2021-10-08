using serverside_servico.Infraestrutura.Mapeamentos;
using serverside_servico.Negocio;

namespace serverside_servico.Mapeamentos
{
    public class FuncionarioMap : ObjetoComCodigoNumericoMap<Funcionario>
    {
        public FuncionarioMap()
        {
            Table("FUNCIONARIO");

            Map(funcionario => funcionario.Nome, "NOME").Length(100).Not.Nullable();
            References(funcionario => funcionario.Departamento, "IDDEPARTAMENTO");
        }
    }
}
