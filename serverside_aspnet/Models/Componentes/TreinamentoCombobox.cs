namespace serverside_aspnet.Models.Componentes
{
    public class TreinamentoCombobox : TreinamentoModel
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public string CampoChave { get; set; }

        public string[] Colunas { get; set; }
    }
}
