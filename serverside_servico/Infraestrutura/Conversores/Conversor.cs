using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Objetos;
using serverside_servico.Infraestrutura.Utilitarios;

namespace serverside_servico.Infraestrutura.Conversores
{
    public abstract class Conversor<TDto, TObjeto> : IConversor<TDto, TObjeto>
        where TDto : DtoPadrao
        where TObjeto : ObjetoCompartilhado
    {
        private readonly IMapper _mapper;

        public Conversor(IMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual TDto Converta(TObjeto objeto)
        {
            if (objeto == null)
            {
                return null;
            }

            var dto = _mapper.ConvertaParaDto<TObjeto, TDto>(objeto);
            return dto;
        }

        public virtual TObjeto Converta(TDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var objeto = _mapper.ConvertaParaObjeto<TDto, TObjeto>(dto);
            AcaoAposConverterDeDtoParaObjeto(dto, objeto);

            return objeto;
        }

        public virtual TObjeto ConvertaParaObjetoPersistido(TDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var objeto = ObtenhaObjetoPersistido(dto);

            if (objeto == null)
            {
                return null;
            }

            _mapper.ConvertaParaObjetoPersistido(dto, objeto);

            AcaoAposConverterDeDtoParaObjetoPersistido(dto, objeto);

            return objeto;
        }

        public virtual List<TDto> Converta(List<TObjeto> listaDeObjeto)
        {
            if (listaDeObjeto == null)
            {
                return null;
            }

            var listaDeDto = listaDeObjeto.Select(Converta).ToList();
            return listaDeDto;
        }

        public virtual List<TObjeto> Converta(List<TDto> listaDeDto)
        {
            if (listaDeDto == null)
            {
                return null;
            }

            var listaDeObjeto = listaDeDto.Select(Converta).ToList();
            return listaDeObjeto;
        }

        protected abstract TObjeto ObtenhaObjetoPersistido(TDto dto);

        protected virtual void AcaoAposConverterDeDtoParaObjeto(TDto dto, TObjeto objeto)
        {
        }

        protected virtual void AcaoAposConverterDeDtoParaObjetoPersistido(TDto dto, TObjeto objeto)
        {
        }
    }
}
