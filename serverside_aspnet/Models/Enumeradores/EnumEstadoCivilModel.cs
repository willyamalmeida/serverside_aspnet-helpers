using System.ComponentModel.DataAnnotations;

namespace serverside_aspnet.Models.Enumeradores
{
    public enum EnumEstadoCivilModel
    {
        [Display(Name = "Solteiro")]
        SOLTEIRO,

        [Display(Name = "Casado")]
        CASADO,

        [Display(Name = "Viúvo")]
        VIUVO
    }
}
