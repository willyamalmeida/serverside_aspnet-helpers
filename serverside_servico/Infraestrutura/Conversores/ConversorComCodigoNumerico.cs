using AutoMapper;
using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Objetos;
using serverside_servico.Infraestrutura.Repositorios;

namespace serverside_servico.Infraestrutura.Conversores
{
    public abstract class ConversorComCodigoNumerico<TDto, TObjeto> : Conversor<TDto, TObjeto>, IConversorComCodigoNumerico<TDto, TObjeto>
        where TDto : DtoComCodigoNumerico
        where TObjeto : ObjetoComCodigoNumerico
    {
        private IRepositorioComCodigoNumerico<TObjeto> _repositorio;

        public ConversorComCodigoNumerico(IRepositorioComCodigoNumerico<TObjeto> repositorio, IMapper mapper)
            : base(mapper)
        {
            _repositorio = repositorio;
        }

        protected override TObjeto ObtenhaObjetoPersistido(TDto dto)
        {
            var objeto = _repositorio.Consulte(dto.Codigo);
            return objeto;
        }
    }
}
