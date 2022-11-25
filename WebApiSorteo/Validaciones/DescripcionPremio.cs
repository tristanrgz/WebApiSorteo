using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace WebApiSorteo.Validaciones
{
    public class DescripcionPremio : ValidationAttribute
    {
        protected override ValidationResult IsValid(object valor, ValidationContext validationContext)
        {
            if(valor == null || string.IsNullOrEmpty(valor.ToString()))
            {
                return new ValidationResult("Digite algun nombre para el premio");
            }

            var elementos = valor.ToString().Split(' ');

            if (!elementos[0].All(char.IsDigit))
            {
                return new ValidationResult("digite la cantidad del objeto a ser regalado en numero");
            }

            for(int i = 1; i < elementos.Length; i++)
            {
                if (!Regex.IsMatch(elementos[i], @"[a-z]"))
                {
                    return new ValidationResult("Digite de forma correcta el objeto regalado");
                }
            }

            return ValidationResult.Success;
        }

    }
}
