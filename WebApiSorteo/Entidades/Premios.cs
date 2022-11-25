using System.ComponentModel.DataAnnotations;
using WebApiSorteo.Entidades;


namespace WebApiSorteo.Entidades
{
    public class Premios : IValidatableObject
    {
        public int Id { get; set; }
        public string descripcion { get; set; }
        public int nivel { get; set; }
        public int SorteoId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (nivel == 0)
            {
                yield return new ValidationResult("Por favor ingrese algun numero",
                new String[] { nameof(nivel) });
            }
        }

    }
}
