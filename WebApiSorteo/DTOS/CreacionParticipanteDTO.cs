using System.ComponentModel.DataAnnotations;
namespace WebApiSorteo.DTOS
{
    public class CreacionParticipanteDTO : IValidatableObject
    {
        [Required]
        public string num_telefono { get;set;}
        [Required]
        [CreditCard]
        public string tarjeta_credito { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!string.IsNullOrEmpty(num_telefono))
            {
                if (!(num_telefono[2] == '-' || num_telefono[3] == '-'))
                {
                    yield return new ValidationResult("El numero telefonico debe dividir la lada y los otros 7 u 8 numeros",
                       new String[] { nameof(num_telefono) });
                }

                var numeros = num_telefono.Split('-');

                switch(numeros[0])
                {
                    case "81":
                    case "55":
                    case "33":
                        if (numeros[1].Length != 8)
                        {
                            yield return new ValidationResult("La longuitud debe ser de 8 digitos",
                            new String[] { nameof(num_telefono) });
                        }
                        break;
                    case "222":
                    case "782":
                    case "667":
                        if (numeros[1].Length != 7)
                        {
                            yield return new ValidationResult("La longitud debe ser de 7 numeros",
                            new String[] { nameof(num_telefono) });
                        }
                        break;
                    default:
                        yield return new ValidationResult("La Extension Lada no es valida",
                        new String[] { nameof(num_telefono) });
                        break;
                }
            }
        }
    }
}
