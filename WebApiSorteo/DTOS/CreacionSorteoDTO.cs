using System.ComponentModel.DataAnnotations;
using WebApiSorteo.Validaciones;
namespace WebApiSorteo.DTOS
{
    public class CreacionSorteoDTO
    {
        [Required]
        [NombreSorteo]
        public string Nombre{ get; set; }
        [Required]
        
        public DateTime Fecha_Inicio_Inscripcion { get; set; }
        [Required]
        
        public DateTime Fecha_Cierre_Inscripcion { get; set; }
        [Required]
        
        public DateTime Fecha_sorteo { get; set; }
       
    }
}
