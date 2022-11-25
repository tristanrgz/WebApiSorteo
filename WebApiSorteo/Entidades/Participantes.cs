using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebApiSorteo.Entidades;


namespace WebApiSorteo.Entidades
{
    public class Participantes : IValidatableObject
    {

        public int Id { get; set; }
        [Required]
        [CreditCard]
        public string tarjeta_credito { get; set; }
        public string num_telefono { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string .IsNullOrEmpty(num_telefono))
            {
                if (!(num_telefono[2] == '-' || num_telefono[3] == '-'))
                {
                    yield return new ValidationResult("El numero de telefono debe dividir la lada y los otros 8 u 7 numeros",
                        new String[] { nameof(num_telefono) });
                }

                var numeros = num_telefono.Split('-');

                switch (numeros[0])
                {
                    case "81":
                    case "55":
                    case "33":
                        if (numeros[1].Length != 8)
                        {
                            yield return new ValidationResult("la longitud debe ser de 8 numeros",
                            new String[] { nameof(num_telefono) });
                        }
                        break;

                    case "614":
                    case "662":
                    case "868":
                        if (numeros[1].Length != 7)
                        {
                            yield return new ValidationResult("la longuitud debe ser de 7 numeros",
                            new String[] { nameof(num_telefono) });
                        }
                        break;
                    default:
                        yield return new ValidationResult("la extension lada no es valida",
                        new String[] { nameof(num_telefono) });
                        break;
                }
            }
        }
    }
}
