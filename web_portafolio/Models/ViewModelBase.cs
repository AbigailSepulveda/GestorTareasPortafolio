using System.ComponentModel.DataAnnotations;

namespace SGM_INSPECCION_DIGITAL.Models {
    public abstract class ViewModelBase {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Rut { get; set; }
        public string Type { get; set; }

        [Required]
        [Display(Prompt = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Prompt = "Contraseña")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

    }

    public class HomeViewModel : ViewModelBase {
    }
}