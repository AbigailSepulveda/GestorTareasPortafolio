using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace web_portafolio.Models {
    public class LoginView {
        [Required]
        [Display(Prompt = "Usuario")]
        [DataType(DataType.Text)]
        public string User { get; set; }

        [Required]
        [Display(Prompt = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        public int idUser { get; set; }
        public int type { get; set; }
        public string Name { get; set; }
        public string Rut { get; set; }
        public string Email { get; set; }

    }
}