using System;
using System.Linq.Expressions;
using serverside_servico.Infraestrutura.Objetos;

namespace serverside_servico.Infraestrutura.Repositorios
{
    public interface IRepositorioComCodigoNumerico<TObjeto> : IRepositorio<TObjeto> where TObjeto : ObjetoComCodigoNumerico
    {
        TObjeto Consulte(int codigo);

        ObjetoPaginado<TObjeto> ConsulteParcial(string filtro, int pagina, int quantidade, Func<string, Expression<Func<TObjeto, bool>>> obtenhaFiltro);

        ObjetoPaginado<TObjeto> ConsultePaginada(string filtro, int pagina, int quantidade, Func<string, Expression<Func<TObjeto, bool>>> obtenhaFiltro);
    }
}
