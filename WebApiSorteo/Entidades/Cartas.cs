using System.ComponentModel.DataAnnotations;
using WebApiSorteo.Validaciones;
namespace WebApiSorteo.Entidades
{
    public class Cartas : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [NombreCarta]
        public string Nombre { get; set; }
        [Required]
        [NumeroCarta]
        public int Numero { get; set;}

        public List<ParticipantesCartasSorteo>? ParticipantesCartasSorteo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre) && Numero != 0)
            {
                switch (Nombre)
                {
                    case "El gallo":
                        if (Numero != 1)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El diablito":
                        if (Numero != 2)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La dama":
                        if (Numero != 3)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El catrin":
                        if (Numero != 4)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El paraguas":
                        if (Numero != 5)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La sirena":
                        if (Numero != 6)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La escalera":
                        if (Numero != 7)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La botella":
                        if (Numero != 8)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El barril":
                        if (Numero != 9)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El arbol":
                        if (Numero != 10)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El melon":
                        if (Numero != 11)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El valiente":
                        if (Numero != 12)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El gorrito":
                        if (Numero != 13)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La muerte":
                        if (Numero != 14)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La pera":
                        if (Numero != 15)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La bandera":
                        if (Numero != 16)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El bandolon":
                        if (Numero != 17)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El violoncello":
                        if (Numero != 18)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La Garza":
                        if (Numero != 19)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El pajaro":
                        if (Numero != 20)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "la mano":
                        if (Numero != 21)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La bota":
                        if (Numero != 22)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La luna":
                        if (Numero != 23)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El Cotorro":
                        if (Numero != 24)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El borracho":
                        if (Numero != 25)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El negrito":
                        if (Numero != 26)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El corazon":
                        if (Numero != 27)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La sandia":
                        if (Numero != 28)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El tambor":
                        if (Numero != 29)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El camaron":
                        if (Numero != 30)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "Las jaras":
                        if (Numero != 31)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El musico":
                        if (Numero != 32)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La araña":
                        if (Numero != 33)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El soldado":
                        if (Numero != 34)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La estrella":
                        if (Numero != 35)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El venado":
                        if (Numero != 36)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El mundo":
                        if (Numero != 37)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El apache":
                        if (Numero != 30)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El nopal":
                        if (Numero != 39)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El alacran":
                        if (Numero != 40)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La rosa":
                        if (Numero != 41)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La calavera":
                        if (Numero != 42)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La campana":
                        if (Numero != 43)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El cantarito":
                        if (Numero != 44)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El sol":
                        if (Numero != 45)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El cazo":
                        if (Numero != 46)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La corona":
                        if (Numero != 47)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La chalupa":
                        if (Numero != 48)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El pino":
                        if (Numero != 49)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El pescado":
                        if (Numero != 50)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La palma":
                        if (Numero != 51)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La maceta":
                        if (Numero != 52)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "El arpa":
                        if (Numero != 53)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    case "La rana":
                        if (Numero != 54)
                        {
                            {
                                yield return new ValidationResult("El numero y el nombre no corresponden",
                                new String[] { nameof(Nombre) });
                            }
                        }
                        break;

                    default:
                        yield return new ValidationResult("La Carta no existe dentro de la loteria mexicana",
                        new String[] { nameof(Nombre) });
                        break;
                }
            }
        }
    }
}
