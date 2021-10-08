using System.ComponentModel;
using serverside_aspnet.Models.Enumeradores;

namespace serverside_aspnet.Models
{
    public class PessoaModel
    {
        public string Nome { get; set; }

        [DisplayName("Estado civil")]
        public EnumEstadoCivilModel EstadoCivil { get; set; }

        public ContatoModel Contato { get; set; }

        [DisplayName("Endereço")]
        public EnderecoModel Endereco { get; set; }
    }
}
