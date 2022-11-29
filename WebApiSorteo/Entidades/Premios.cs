using System.ComponentModel.DataAnnotations;
using WebApiSorteo.Entidades;
using WebApiSorteo.Validaciones;



namespace WebApiSorteo.Entidades
{
    public class Premios : IValidatableObject
    {
        public int Id { get; set; }
        [DescripcionPremio]
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public int SorteoId { get; set; }
        public Sorteo? Sorteo { get; set; } 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Nivel == 0)
            {
                yield return new ValidationResult("Digite un numero",
                new String[] { nameof(Nivel) });
            }

            if (Nivel >= 1 || Nivel <= 6)
            {
                yield return new ValidationResult("Digite un nivel valido",
                new String[] { nameof(Nivel) });
            }
        }

    }
}
