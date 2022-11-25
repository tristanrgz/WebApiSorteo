using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSorteo.Entidades;
namespace WebApiSorteo.Controllers
{
    [ApiController]
    [Route("SistemaDeInscripciones/")]
    public class ParticipantesController: ControllerBase
    {
        private readonly ApplicationDbContext dBContext;
        private readonly IMapper mapper;

        public ParticipantesController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.dBContext = context;
        }
    }
}
