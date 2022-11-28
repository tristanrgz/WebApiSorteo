namespace WebApiSorteo.Seguridad
{
    public class RespuestaDeAutenticacion
    {
        public DateTime FechaCaducidad { get; set; }
        public string Token { get; set; }
    }
}
