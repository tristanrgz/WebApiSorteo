using System.ComponentModel.DataAnnotations;
using WebApiSorteo.Validaciones;
namespace WebApiSorteo.DTOS
{
    public class CreacionPremioDTO : IValidatableObject
    {
        [DescripcionPremio]
        public string descripcion { get; set; }
        public int nivel { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (nivel == 0)
            {
                yield return new ValidationResult("Digite un  numero",
                new String[] { nameof(nivel) });
            }

            if (!(nivel >= 1) || !(nivel <= 6))
            {
                yield return new ValidationResult("Digite  un  nivel valido",
                new String[] { nameof(nivel) });
            }
        }
    }
}
