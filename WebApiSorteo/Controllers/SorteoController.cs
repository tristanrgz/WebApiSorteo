using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSorteo.DTOS;
using WebApiSorteo.Entidades;

namespace WebApiSorteo.Controllers

{
    [ApiController]
    [Route("api/Sorteos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
    public class SorteoController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly ILogger<SorteoController> logger;
        private readonly IMapper mapper;

        public SorteoController(ApplicationDbContext dbContext, ILogger<SorteoController> logger, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet("ObtenerPremios/Sorteo/{id:int}", Name = "GetSorteo")]
        public async Task<ActionResult<SorteoDTOconPremios>> Get(int id)
        {
            var sorteo = await DbContext.Sorteo
                .Include(p => p.Premios)
                .FirstOrDefaultAsync(x => x.Id == id);
            logger.LogInformation("Aqui esta el elemento de la base de datos");
            logger.LogInformation(sorteo.ToString());
            if (sorteo == null)
            {
                logger.LogInformation("No salio nada");
                return NotFound("La rifa no se encontro");
            }
            logger.LogInformation("Paso el if");
            return mapper.Map<SorteoDTOconPremios>(sorteo);
        }

        [HttpGet("ObtenerCartasDisponibles/Sorteo/{id:int}")]
        public async Task<ActionResult<SorteoDTOconCarta>> GetCartas(int id)
        {
            var sorteo = await DbContext.Sorteo
                .Include(x => x.ParticipantesCartasSorteo)
                .ThenInclude(x => x.Cartas)
                .FirstOrDefaultAsync(x => x.Id == id);
            logger.LogInformation("informacion extraida");
            logger.LogInformation(sorteo.ToString());

            if(sorteo == null)
            {
                logger.LogInformation("No habia nada");
                return NotFound("La rifa no fue encontrada");
            }
            var baraja = await DbContext.Cartas
                .ExceptBy(sorteo.ParticipantesCartasSorteo.Select(c => c.Cartas)
                , x => x).ToListAsync();
            if (baraja.Count() != 54)
            {
                return BadRequest("Las cartas no se han introducido");
            }

            var SorteosFaltantes = new Sorteo()
            {
                Id = sorteo.Id,
                Nombre = sorteo.Nombre,
                Inicio_Inscripcion = sorteo.Inicio_Inscripcion,
                Cierre_Inscripcion = sorteo.Cierre_Inscripcion,
                Fecha_Sorteo = sorteo.Fecha_Sorteo,
                Premios = sorteo.Premios,
                ParticipantesCartasSorteo = new List<ParticipantesCartasSorteo>() { }
            };
            foreach (var i in baraja)
            {
                SorteosFaltantes.ParticipantesCartasSorteo.Add(new ParticipantesCartasSorteo
                {
                    IdCartas = i.Id.ToString(),
                    IdSorteo = SorteosFaltantes.Id.ToString(),
                    Cartas = i,
                    Sorteo = SorteosFaltantes
                });
            }

            logger.LogInformation("Inforacion recreada");
            logger.LogInformation(SorteosFaltantes.ToString());
            return mapper.Map<SorteoDTOconCarta>(SorteosFaltantes);
        }
        [HttpPost("/CreacionDeSorteo")]
        public async Task<ActionResult> Post(CreacionSorteoDTO SorteoCreacionDTO)
        {
            var existe = await DbContext.Sorteo.AnyAsync(x => x.Nombre == SorteoCreacionDTO.Nombre);
            logger.LogInformation("Se consiguio de la base de datos");
            logger.LogInformation(existe.ToString());

            if (existe)
            {
                return BadRequest("Ya hay una rifa con ese nombre");
            }
            if (SorteoCreacionDTO.Premio.Count != 6)
            {
                return BadRequest("Los premios no cuentan con la cantidad estipulada para la rifa");
            }
            for (var i = 0; i < 5; i++)
            {
                for (var j = i + 1; j < 6; j++)
                {
                    if (SorteoCreacionDTO.Premio[i].nivel == SorteoCreacionDTO.Premio[j].nivel)
                    {
                        return BadRequest("Los niveles de los premios estan repetidos");
                    }
                }
            }
            var nuevoElemento = mapper.Map<Sorteo>(SorteoCreacionDTO);
            logger.LogInformation("Se realizo el mapeo");
            logger.LogInformation(nuevoElemento.ToString());
            DbContext.Add(nuevoElemento);
            foreach (var r in nuevoElemento.Premios)
            {
                DbContext.Add(r);
            }
            logger.LogInformation("Se agrego el elemento a la base de datos");
            await DbContext.SaveChangesAsync();
            var nuevaVista = mapper.Map<SorteoDTOconPremios>(nuevoElemento);
            logger.LogInformation("se envia la confirmacion de la creacion");
            logger.LogInformation(nuevaVista.ToString());
            return CreatedAtRoute("GetSorteo", new { id = nuevoElemento.Id }, nuevaVista);
        }
        [HttpPost("/AsignacionDeCartas")]
        public async Task<ActionResult> PostCartas(CreacionCartaDTO cartasDTO)
        {
            var exist = await DbContext.Cartas.AnyAsync(x => x.Numero == cartasDTO.numero);
            if (exist)
            {
                return BadRequest("Ya existe esa carta");
            }
            var nuevoElemento = mapper.Map<Cartas>(cartasDTO);
            DbContext.Add(nuevoElemento);
            logger.LogInformation("Se inserto el elemento a la base de datos");
            await DbContext.SaveChangesAsync();
            var nuevaVista = mapper.Map<CartaDTO>(nuevoElemento);
            logger.LogInformation(" Enviamos  la confirmacion de la creacion");
            logger.LogInformation(nuevaVista.ToString());
            return CreatedAtRoute("GetSorteo", new { id = nuevoElemento.Id }, nuevaVista);
        }
        [HttpDelete("/EliminacionDeSorteo/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await DbContext.Sorteo.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("El Recurso no fue encontrado.");
            }
            var rifaBorrar = await DbContext.Sorteo.Include(x => x.Premios).FirstAsync(x => x.Id == id);
            DbContext.Remove(rifaBorrar);

            await DbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
