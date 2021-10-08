using System;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Objetos;

namespace serverside_servico.Infraestrutura.Utilitarios
{
    public static class AutoMapperExtensions
    {
        public static TDto ConvertaParaDto<TObjeto, TDto>(this IMapper mapper, TObjeto objeto)
            where TObjeto : ObjetoCompartilhado
            where TDto : DtoPadrao
        {
            var dto = mapper.Map<TObjeto, TDto>(objeto);
            return dto;
        }

        public static TObjeto ConvertaParaObjeto<TDto, TObjeto>(this IMapper mapper, TDto dto)
            where TDto : DtoPadrao
            where TObjeto : ObjetoCompartilhado
        {
            var objeto = mapper.Map<TDto, TObjeto>(dto);
            return objeto;
        }

        public static TObjeto ConvertaParaObjetoPersistido<TDto, TObjeto>(this IMapper mapper, TDto dto, TObjeto objeto)
            where TDto : DtoPadrao
            where TObjeto : ObjetoCompartilhado
        {
            mapper.Map(dto, objeto);
            return objeto;
        }

        public static IMappingExpression<TOrigem, TDestino> IgnorePropriedade<TOrigem, TDestino>(
            this IMappingExpression<TOrigem, TDestino> map,
            Expression<Func<TDestino, object>> seletor)
        {
            map.ForMember(seletor, opt =>
            {
                opt.UseDestinationValue();
                opt.Ignore();
            });

            return map;
        }

        public static IMappingExpression<TOrigem, TDestino> IgnoreTodasPropriedadesNaoExistente<TOrigem, TDestino>(
            this IMappingExpression<TOrigem, TDestino> map)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var origemType = typeof(TOrigem);
            var propriedadesDeDestino = typeof(TDestino).GetProperties(flags);

            foreach (var propriedade in propriedadesDeDestino)
            {
                if (origemType.GetProperty(propriedade.Name, flags) == null)
                {
                    map.ForMember(propriedade.Name, opt => opt.Ignore());
                }
            }

            return map;
        }
    }
}
