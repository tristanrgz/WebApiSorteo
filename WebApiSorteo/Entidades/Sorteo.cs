using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebApiSorteo.Entidades;

namespace WebApiSorteo.Entidades
{
    public class Sorteo
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set;}
        [Required]
        //Esta es la fecha en que se inicia la inscripcion para el sorteo
        public DateTime Inicio_Inscripcion { get; set; }
        [Required]
        // Esta es la fecha en que se cierra la inscripcion para el sorteo
        public DateTime Cierre_Inscripcion { get; set; }
        [Required]
        //Esta si es la fecha en que se efectua el sorteo
        public DateTime Fecha_Sorteo { get; set; }
        
    }
}
