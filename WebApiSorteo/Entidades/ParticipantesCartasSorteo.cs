namespace WebApiSorteo.Entidades
{
    public class ParticipantesCartasSorteo
    {
        public string IdParticipantes { get; set; }
        public string IdCartas { get; set; }
        public string IdSorteo { get; set; }

        public Cartas Cartas { get; set; }
        public Participantes Participantes { get; set; }
        public Sorteo Sorteo { get; set; }
    }
}
