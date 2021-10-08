using serverside_servico.Dtos;
using serverside_servico.Infraestrutura.Conversores;
using serverside_servico.Negocio;

namespace serverside_servico.Conversores
{
    public class AutoMapperDepartamento : AutoMapperProfileComCodigoNumerico<DtoDepartamento, Departamento>
    {
    }
}
