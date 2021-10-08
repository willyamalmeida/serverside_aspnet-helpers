using System.Collections.Generic;
using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Objetos;

namespace serverside_servico.Infraestrutura.Conversores
{
    public interface IConversor<TDto, TObjeto>
        where TDto : DtoPadrao
        where TObjeto : ObjetoCompartilhado
    {
        TDto Converta(TObjeto objeto);

        TObjeto Converta(TDto dto);

        TObjeto ConvertaParaObjetoPersistido(TDto dto);

        List<TDto> Converta(List<TObjeto> listaDeObjeto);

        List<TObjeto> Converta(List<TDto> listaDeDto);
    }
}
