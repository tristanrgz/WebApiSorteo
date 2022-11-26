using System.ComponentModel.DataAnnotations;

namespace WebApiSorteo.Validaciones
{
    public class NombreRifa : ValidationAttribute
    {
        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Porfavor ingrese algun nombre para la rifa");
            }

            var elementos = value.ToString().Split(' ');
            if (!(elementos[0].Equals("Rifa")))
            {
                return new ValidationResult("El nombre debe iniciar con la palabra rifa");
            }
            return ValidationResult.Success;
        }
    }
}
