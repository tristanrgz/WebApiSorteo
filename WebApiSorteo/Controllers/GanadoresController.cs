using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSorteo.DTOS;
using WebApiSorteo.Entidades;
using WebApiSorteo.Servicios;

namespace WebApiSorteo.Controllers
{
    [ApiController]
    [Route("ResultadosDeSorteo/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]

    public class GanadoresController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IService service;
        private readonly IWebHostEnvironment env;
        private readonly string archivoGanadores = "RegistrosDeLlamadas.txt";

        public GanadoresController(ApplicationDbContext context, IWebHostEnvironment env,
            IService service)
        {
            this.DbContext = context;
            this.env = env;
            this.service = service;
        }
        [HttpGet("CalculoDeGanadorEnSorteo/{id:int}")]

        public async Task<ActionResult<List<ParticipantesDTO>>> Post(int id)
        {
            var sorteo = DbContext.Sorteo
                .Include(p => p.Premios)
                .Include(pcs => pcs.ParticipantesCartasSorteo)
                .ThenInclude(p => p.Participantes)
                .FirstOrDefault(x => x.Id == id);

            if (sorteo == null)
            {
                return NotFound("No se encontro la rifa ");
            }
            if (sorteo.Premios.Count != 6)
            {
                return NotFound("No tienes los premios suficientes");
            }

            System.Console.WriteLine($"<{sorteo.ParticipantesCartasSorteo.Count}>");
            if (sorteo.ParticipantesCartasSorteo.Count != 54)
            {
                return NotFound("No tienes los suficientes participantes");
            }
            var deck = await DbContext.Cartas.ToListAsync();
            var con = 6;
            deck = service.EjecutarSeleccion(deck);
            var lista = new List<GanadoresDTO>();
            foreach (var c in deck.Take(6))
            {
                var participanteGanador = sorteo.ParticipantesCartasSorteo.FirstOrDefault(x => (x.IdSorteo == id.ToString())
                && (x.IdCartas == c.Id.ToString()));
                var participante = await DbContext.Participantes.FirstAsync
                    (x => x.Id.ToString() == participanteGanador.IdParticipantes);
                var premios = sorteo.Premios.First(p => p.Nivel == con);
                lista.Add(new GanadoresDTO
                {
                    NombreSorteo = sorteo.Nombre,
                    Participantes = new ParticipantesDTO()
                    {
                        num_telefono = participante.num_telefono,
                        tarjeta_credito = participante.tarjeta_credito
                    },
                    Premios = new PremiosDTO()
                    {
                        Descripcion = premios.Descripcion,
                        Nivel = premios.Nivel
                    }
                });
                con--;
                
            }
            foreach (var e in lista)
            {
                var ruta = $@"{env.ContentRootPath}\wwwroot\{archivoGanadores}";
                using (StreamWriter writer = new StreamWriter(ruta, append: true))
                { writer.WriteLine($@"Llamada:{e.Participantes.num_telefono},Premio:{e.Premios.Descripcion}"); }
            }
            return Ok(lista);

        }

       
    }
}
