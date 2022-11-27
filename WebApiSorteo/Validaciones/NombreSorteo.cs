using System.ComponentModel.DataAnnotations;

namespace WebApiSorteo.Validaciones
{
    public class NombreSorteo : ValidationAttribute
    {
        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Digite un nombre para el sorteo");
            }

            var elementos = value.ToString().Split(' ');
            if (!(elementos[0].Equals("Sorteo")))
            {
                return new ValidationResult("El nombre debe iniciar con la palabra Sorteo");
            }
            return ValidationResult.Success;
        }
    }
}
