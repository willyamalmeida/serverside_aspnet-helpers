using System;
using System.Linq.Expressions;
using serverside_servico.Dtos;
using serverside_servico.Infraestrutura.Conversores;
using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Servicos;
using serverside_servico.Interfaces.Repositorios;
using serverside_servico.Interfaces.Servicos;
using serverside_servico.Negocio;

namespace serverside_servico.Servicos
{
    public class ServicoDeFuncionario : ServicoComCodigoNumerico<DtoFuncionario, Funcionario>, IServicoDeFuncionario
    {
        public ServicoDeFuncionario(IRepositorioFuncionario repositorio, IConversorComCodigoNumerico<DtoFuncionario, Funcionario> conversor)
            : base(repositorio, conversor)
        {
        }

        public override DtoPaginado<DtoFuncionario> ConsulteParcial(string filtro, int pagina, int quantidade)
        {
            var resultado = ((IRepositorioFuncionario)Repositorio).ConsulteParcial(
                filtro,
                pagina,
                quantidade,
                ObtenhaExpressao);

            var conversorFuncionario = (IConversorComCodigoNumerico<DtoFuncionario, Funcionario>)Conversor;
            var conversorPaginado = new ConversorPaginado<DtoFuncionario, Funcionario>(conversorFuncionario);

            var dtoPaginado = conversorPaginado.ConvertaParaDto(resultado);

            return dtoPaginado;
        }

        public override DtoPaginado<DtoFuncionario> ConsultePaginado(string filtro, int pagina, int quantidade)
        {
            var resultado = ((IRepositorioFuncionario)Repositorio).ConsultePaginada(filtro, pagina, quantidade, ObtenhaExpressao);

            var conversorFuncionario = (IConversorComCodigoNumerico<DtoFuncionario, Funcionario>)Conversor;
            var conversorPaginado = new ConversorPaginado<DtoFuncionario, Funcionario>(conversorFuncionario);

            var dtoPaginado = conversorPaginado.ConvertaParaDto(resultado);

            return dtoPaginado;
        }

        private Expression<Func<Funcionario, bool>> ObtenhaExpressao(string filtro)
        {
            if (int.TryParse(filtro, out int codigo))
            {
                return x => x.Codigo == codigo;
            }

            return x => x.Nome.ToUpperInvariant().StartsWith(filtro);
        }
    }
}
