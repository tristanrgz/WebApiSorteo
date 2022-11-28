using System.ComponentModel.DataAnnotations;

namespace WebApiSorteo.Seguridad
{
    public class CredencialesDeUsuario
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
