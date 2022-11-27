using System.ComponentModel.DataAnnotations;
using WebApiSorteo.Validaciones;
namespace WebApiSorteo.DTOS
{
    public class SorteoPatchDTO
    {
        [Required]
        [NombreSorteo]
        public string Nombre { get; set; }
        public DateTime Fecha_Inicio_Inscripcion { get; set; }
        [Required]
        // Esta es la fecha en que se cierra la inscripcion para el sorteo
        public DateTime Fecha_Cierre_Inscripcion { get; set; }
        [Required]
        //Esta si es la fecha en que se efectua el sorteo
        public DateTime Fecha_sorteo { get; set; }
    }
}
