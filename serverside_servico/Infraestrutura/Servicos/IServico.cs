using System.Collections.Generic;
using serverside_servico.Infraestrutura.Dtos;

namespace serverside_servico.Infraestrutura.Servicos
{
    public interface IServico<TDto> where TDto : DtoPadrao
    {
        void Cadastre(TDto dto);

        void Atualize(TDto dto);

        List<TDto> Consulte(string filtro, int quantidade);

        List<TDto> ConsulteLista();
    }
}
