using AutoMapper;
using serverside_servico.Dtos;
using serverside_servico.Infraestrutura.Conversores;
using serverside_servico.Interfaces.Repositorios;
using serverside_servico.Negocio;

namespace serverside_servico.Conversores
{
    public class ConversorDepartamento : ConversorComCodigoNumerico<DtoDepartamento, Departamento>
    {
        public ConversorDepartamento(IRepositorioDepartamento repositorio, IMapper mapper)
            : base(repositorio, mapper)
        {
        }
    }
}
