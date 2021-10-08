using System;
using System.Collections.Generic;
using serverside_servico.Infraestrutura.Conversores;
using serverside_servico.Infraestrutura.Dtos;
using serverside_servico.Infraestrutura.Objetos;
using serverside_servico.Infraestrutura.Repositorios;

namespace serverside_servico.Infraestrutura.Servicos
{
    public abstract class Servico<TDto, TObjeto> : IServico<TDto>
        where TDto : DtoPadrao
        where TObjeto : ObjetoCompartilhado
    {
        public Servico(IRepositorio<TObjeto> repositorio, IConversor<TDto, TObjeto> conversor)
        {
            Repositorio = repositorio;
            Conversor = conversor;
        }

        protected IRepositorio<TObjeto> Repositorio { get; private set; }

        protected IConversor<TDto, TObjeto> Conversor { get; private set; }

        public virtual void Cadastre(TDto dto)
        {
            ExecuteServico(
                () =>
                {
                    var objeto = Conversor.Converta(dto);
                    ValideCadastro(objeto);
                    Repositorio.Cadastre(objeto);
                });
        }

        public virtual void Atualize(TDto dto)
        {
            ExecuteServico(
                () =>
                {
                    var objeto = Conversor.ConvertaParaObjetoPersistido(dto);
                    ValideAtualizacao(objeto);
                    Repositorio.Atualize(objeto);
                });
        }

        public virtual List<TDto> Consulte(string filtro, int quantidade)
        {
            throw new NotImplementedException();
        }

        public virtual List<TDto> ConsulteLista()
        {
            var listaObjeto = Repositorio.ConsulteLista();
            var listaDto = Conversor.Converta(listaObjeto);
            return listaDto;
        }

        protected virtual void ValideCadastro(TObjeto objeto)
        {
        }

        protected virtual void ValideAtualizacao(TObjeto objeto)
        {
        }

        protected virtual void ValideExclusao(TObjeto objeto)
        {
        }

        protected void ExecuteServico(Action executeServico)
        {
            new ControladorDeTransacao().Execute(executeServico);
        }
    }
}
