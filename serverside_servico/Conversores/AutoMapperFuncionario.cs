using AutoMapper;
using serverside_servico.Dtos;
using serverside_servico.Infraestrutura.Conversores;
using serverside_servico.Infraestrutura.Utilitarios;
using serverside_servico.Negocio;

namespace serverside_servico.Conversores
{
    public class AutoMapperFuncionario : AutoMapperProfileComCodigoNumerico<DtoFuncionario, Funcionario>
    {
        protected override void IgnorePropriedades(IMappingExpression<DtoFuncionario, Funcionario> map)
        {
            map.IgnorePropriedade(x => x.Departamento);

            base.IgnorePropriedades(map);
        }
    }
}
