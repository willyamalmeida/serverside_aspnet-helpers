using serverside_servico.Infraestrutura.Persistencias;
using serverside_servico.Interfaces.Repositorios;
using serverside_servico.Negocio;

namespace serverside_servico.Repositorios
{
    public class RepositorioDepartamento : RepositorioComCodigoNumerico<Departamento>, IRepositorioDepartamento
    {
    }
}
