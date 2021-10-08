using System;

namespace serverside_aspnet.Models
{
    [Serializable]
    public class ComponenteModel : ComponentePadraoModel
    {
        public int QtdDeItensRetornados { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Colunas { get; set; }

        public string CampoChave { get; set; }

        public string[] CamposTemplate { get; set; }

        public string ItemSelecionado { get; set; }
    }
}
