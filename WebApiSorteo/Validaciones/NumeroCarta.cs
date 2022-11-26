using System.ComponentModel.DataAnnotations;

namespace WebApiSorteo.Validaciones
{
    public class NumeroCarta : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valor = Convert.ToInt32(value);
            if (!(valor >= 1) || !(valor <= 54))
            {
                return new ValidationResult("El numero no se encuentra dentro de los numeros de la loteria");
            }

            return ValidationResult.Success;
        }
    }
}
