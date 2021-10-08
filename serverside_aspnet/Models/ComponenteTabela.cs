using System.Collections.Generic;
using serverside_aspnet.Models.Componentes;

namespace serverside_aspnet.Models
{
    public class ComponenteTabela : ComponentePadraoModel
    {
        public ComponenteTabela()
        {
            Colunas = new List<Coluna>();
        }

        public List<Coluna> Colunas { get; set; }

        public string Controller { get; set; }

        public string ActionConsulte { get; set; }

        public string ActionEditar { get; set; }

        public int Pagina { get; set; }

        public int Quantidade { get; set; }

        public bool HabiliteFiltroDePesquisa { get; set; }
    }
}
