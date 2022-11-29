using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSorteo.DTOS;
using WebApiSorteo.Entidades;
namespace WebApiSorteo.Controllers
{
    [ApiController]
    [Route("SistemaDeInscripciones/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
    public class ParticipantesController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public ParticipantesController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.DbContext = context;
        }

        [HttpGet("ObtenerCliente/{id:int}", Name = "ObtenerParticipante")]
        public async Task<ActionResult<ParticipantesDTO>> Get(int id)
        {
            var participantes = await DbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);
            var usuario = await DbContext.Users.FirstAsync(x => x.Id == participantes.UserId);
            if (usuario == null)
            {
                return NotFound("El ususario correspondiente al particpante no fue encontrado");
            }
            if (participantes == null)
            {
                return BadRequest("El participante no fue encontrado");
            }
            var participantesDTO = mapper.Map<ParticipantesDTO>(participantes);

            return participantesDTO;
        }

        [HttpPost("DardeAltaParticipantes")]
        public async Task<ActionResult> Post(CreacionParticipanteDTO creacionParticipanteDTO)
        {
            var existePar = await DbContext.Participantes.AnyAsync(x => x.tarjeta_credito == creacionParticipanteDTO.tarjeta_credito);
            if (existePar)
            {
                return BadRequest("El participante ya existe ");
            }
            var nuevoElemento = mapper.Map<Participantes>(creacionParticipanteDTO);
            DbContext.Add(nuevoElemento);
            var nuevaVista = mapper.Map<ParticipantesDTO>(nuevoElemento);
            await DbContext.SaveChangesAsync();
            return CreatedAtRoute("ObtenerPartcipante", new { id = nuevoElemento.Id }, nuevaVista);
        }

        [HttpGet("ObtenerInscripcionesDeLosParticipantes/{id:int}", Name = "ObtenerParticipacion")]
        public async Task<ActionResult> GetParticipacion(int id)
        {
            var inscripciones = await DbContext.ParticipantesCartasSorteo.Where(x => x.IdParticipantes == id.ToString()).ToListAsync();
            var salida = new ParticpiantesCartaSorteoDTOSalida();
            foreach (var i in inscripciones)
            {
                salida.CartaDTO.Add(Convert.ToInt32(i.IdCartas));
                salida.SorteoDTO.Add(Convert.ToInt32(i.IdSorteo));
            }
            return Ok(salida);
        }

        [HttpPost("inscripcionSorteo/{idSorteo:int}/Participantes/{idParticipante:int}/Carta/{idCarta:int}")]
        public async Task<ActionResult> Postinscripcion(int idSorteo,int idParticipante, int idCarta)
        {
            var elementoSorteo = await DbContext.Sorteo.AnyAsync(x => x.Id == idSorteo);
            if(!elementoSorteo)
            {
                return BadRequest("No se hallo la Rifa");
            }

            var elementoParticipante = await DbContext.Participantes.AnyAsync(x => x.Id == idParticipante);
            if(!elementoParticipante)
            {
                return BadRequest("No se hallo el participante");
            }

            var elementoCarta = await DbContext.Cartas.AnyAsync(x => x.Id == idCarta);
            if (!elementoCarta)
            {
                return BadRequest("No se hallo la carta");
            }

            try
            {
                var elemento = await DbContext.ParticipantesCartasSorteo.SingleAsync(x => x.IdSorteo == idSorteo.ToString() && x.IdParticipantes == idParticipante.ToString());
                return BadRequest("El participante ya esta participando en esta rifa");
            }

            catch(Exception)
            {
                var cantidadDePraticipantes = await DbContext.ParticipantesCartasSorteo.Where(x => x.IdSorteo == idSorteo.ToString()).ToListAsync();

                if (!(cantidadDePraticipantes.Count() <= 54))
                {
                    return BadRequest("La rifa ya se lleno");
                }
                var validacionCarta = await DbContext.ParticipantesCartasSorteo.AnyAsync(x => x.IdSorteo == idSorteo.ToString() && x.IdCartas == idCarta.ToString());
                if (validacionCarta)
                {
                    return BadRequest("La carta ya esta en juego en la rifa");
                }
                var carta = await DbContext.Cartas.FirstAsync(x => x.Id == idCarta);
                var sorteo = await DbContext.Sorteo.FirstAsync(x => x.Id == idSorteo);
                var participante = await DbContext.Participantes.FirstAsync(x => x.Id == idParticipante);
                var nuevoElemento = new ParticipantesCartasSorteo()
                {
                    IdCartas = carta.Id.ToString(),
                    IdParticipantes = participante.Id.ToString(),
                    IdSorteo = sorteo.Id.ToString(),
                    Cartas = carta,
                    Participantes = participante,
                    Sorteo = sorteo,
                };
                DbContext.Add(nuevoElemento);
                var nuevaVista = mapper.Map<ParticipantesCartaSorteoDTO>(nuevoElemento);
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("ObtenerParticipacion", new { id = Convert.ToInt32(nuevaVista.ParticipanteDTO) }, nuevaVista);
            }
        }

        [HttpPut("ModificarParticipante/{id:int}")]
        public async Task<ActionResult> Put(CreacionParticipanteDTO creacionParticipanteDTO, int id)
        {
            var exist = await DbContext.Participantes.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return BadRequest("El elemento no existe");
            }
            var participante = mapper.Map<Sorteo>(creacionParticipanteDTO);
            participante.Id = id;
            DbContext.Update(participante);
            await DbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
