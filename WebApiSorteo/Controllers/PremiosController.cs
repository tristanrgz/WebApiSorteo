using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApiSorteo.DTOS;
using WebApiSorteo.Entidades;

namespace WebApiSorteo.Controllers
{
    [ApiController]
    [Route("RegistroPremiosDeSorteo/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
    public class PremiosController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public PremiosController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.DbContext = context;
        }
        [HttpPut("ModificarUnPremio/{id:int}")]
        public async Task<ActionResult> Put(int id, CreacionPremioDTO creacionPremiosDTO)
        {
            var existe = await DbContext.Premios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var premios = mapper.Map<Premios>(creacionPremiosDTO);
            premios.Id = id;

            DbContext.Update(premios);
            await DbContext.SaveChangesAsync();
            return NoContent();
        }


    }
}
