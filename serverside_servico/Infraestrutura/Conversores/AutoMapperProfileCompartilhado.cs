using AutoMapper;
using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Objetos;
using serverside_servico.Infraestrutura.Utilitarios;

namespace serverside_servico.Infraestrutura.Conversores
{
    public abstract class AutoMapperProfileCompartilhado<TDto, TObjeto> : Profile
        where TDto : DtoPadrao
        where TObjeto : ObjetoCompartilhado
    {
        protected AutoMapperProfileCompartilhado()
        {
            var map = CreateMap<TDto, TObjeto>()
                .IgnorePropriedade(x => x.Id)
                .IgnoreTodasPropriedadesNaoExistente();

            IgnorePropriedades(map);

            map.ReverseMap();
        }

        protected virtual void IgnorePropriedades(IMappingExpression<TDto, TObjeto> map)
        {
        }
    }
}
