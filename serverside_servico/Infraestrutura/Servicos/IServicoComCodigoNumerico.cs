using serverside_servico.Infraestrutura.Dtos;

namespace serverside_servico.Infraestrutura.Servicos
{
    public interface IServicoComCodigoNumerico<TDto> : IServico<TDto> where TDto : DtoComCodigoNumerico
    {
        TDto Consulte(int codigo);

        void Exclue(int codigo);

        DtoPaginado<TDto> ConsulteParcial(string filtro, int pagina, int quantidade);

        DtoPaginado<TDto> ConsultePaginado(string filtro, int pagina, int quantidade);
    }
}
