using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Objetos;

namespace serverside_servico.Infraestrutura.Conversores
{
    public interface IConversorComCodigoNumerico<TDto, TObjeto> : IConversor<TDto, TObjeto>
        where TDto : DtoComCodigoNumerico
        where TObjeto : ObjetoComCodigoNumerico
    {
    }
}
