﻿using System.ComponentModel.DataAnnotations;
using WebApiSorteo.Entidades;
using WebApiSorteo.Validaciones;



namespace WebApiSorteo.Entidades
{
    public class Premios : IValidatableObject
    {
        public int Id { get; set; }
        [DescripcionPremio]
        public string descripcion { get; set; }
        public int nivel { get; set; }
        public int SorteoId { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (nivel == 0)
            {
                yield return new ValidationResult("Digite un numero",
                new String[] { nameof(nivel) });
            }

            if (nivel >= 1 || nivel <= 6)
            {
                yield return new ValidationResult("Digite un nivel valido",
                new String[] { nameof(nivel) });
            }
        }

    }
}
