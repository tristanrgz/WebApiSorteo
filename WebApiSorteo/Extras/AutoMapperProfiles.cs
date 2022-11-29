using AutoMapper;
using WebApiSorteo.DTOS;
using WebApiSorteo.Entidades;

namespace WebApiSorteo.Extras
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Aqui estan los Mapeos con cartas
            CreateMap<CreacionCartaDTO, Cartas>();
            CreateMap<Cartas, CartaDTO>();
            //Aqui estan los Mapeos de participantes
            CreateMap<CreacionParticipanteDTO, Participantes>();
            CreateMap<Participantes, ParticipantesDTO>();
            //Aqui estan los Mapeos de premios
            CreateMap<CreacionPremioDTO, Premios>();
            CreateMap<PremiosDTO, Premios>();
            //Aqui estan los Mapeos del sorteo
            CreateMap<Sorteo, SorteoDTOconCarta>().ForMember(x => x.CartaDTO,
                opciones => opciones.MapFrom(MapCartasVista));
            CreateMap<Sorteo, SorteoDTOconPremios>().ForMember(x => x.PremiosDTO,
                opciones => opciones.MapFrom(MapPremiosVista));
            //Aqui esta el Mapeo necesario para la creacion de la rifa
            CreateMap<CreacionSorteoDTO, Sorteo>().ForMember(x => x.Premios,
                opciones => opciones.MapFrom(MapCreacionSorteo));
            CreateMap<SorteoPatchDTO, Sorteo>().ReverseMap();
            CreateMap<ParticipantesCartasSorteo, ParticipantesCartaSorteoDTO>();
        }
        private List<Premios> MapCreacionSorteo(CreacionSorteoDTO creacionSorteoDTO, Sorteo sorteo)
        {
            var lista = new List<Premios>();
            if (creacionSorteoDTO.Premio == null)
            {
                return lista;
            }

            foreach (var i in creacionSorteoDTO.Premio)
            {
                lista.Add(new Premios { SorteoId = sorteo.Id, Descripcion = i.descripcion, Nivel = i.nivel });
            }

            return lista;
        }
        private List<CartaDTO> MapCartasVista(Sorteo sorteo, SorteoDTOconCarta sorteoDTOconCartas)
        {
            var lista = new List<CartaDTO>();
            if (sorteo.ParticipantesCartasSorteo == null)
            {
                return lista;
            }
            foreach (var i in sorteo.ParticipantesCartasSorteo)
            {
                lista.Add(new CartaDTO { numero = i.Cartas.Numero, Nombre = i.Cartas.Nombre });
            }
            return lista;
        }
        private List<PremiosDTO> MapPremiosVista(Sorteo sorteo, SorteoDTOconPremios sorteoDTOconPremios)
        {
            var lista = new List<PremiosDTO>();
            if (sorteo.Premios == null)
            {
                return lista;
            }
            foreach (var p in sorteo.Premios)
            {
                lista.Add(new PremiosDTO { Nivel = p.Nivel, Descripcion = p.Descripcion });
            }
            return lista;
        }
    }
}
