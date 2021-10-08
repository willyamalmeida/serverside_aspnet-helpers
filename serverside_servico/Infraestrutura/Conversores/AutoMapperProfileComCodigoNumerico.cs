using AutoMapper;
using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Objetos;
using serverside_servico.Infraestrutura.Utilitarios;

namespace serverside_servico.Infraestrutura.Conversores
{
    public abstract class AutoMapperProfileComCodigoNumerico<TDto, TObjeto> : AutoMapperProfileCompartilhado<TDto, TObjeto>
        where TDto : DtoComCodigoNumerico
        where TObjeto : ObjetoComCodigoNumerico
    {
        protected override void IgnorePropriedades(IMappingExpression<TDto, TObjeto> map)
        {
            map.IgnorePropriedade(x => x.Codigo);
        }
    }
}
