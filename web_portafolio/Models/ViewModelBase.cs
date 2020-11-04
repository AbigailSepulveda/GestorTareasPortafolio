using System.ComponentModel.DataAnnotations;

namespace web_portafolio.Models {
    public abstract class ViewModelBase {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Rut { get; set; }
        public string Type { get; set; }
        public string Modules { get; set; }
        public string Unit { get; set; }
        public string Token { get; set; }

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