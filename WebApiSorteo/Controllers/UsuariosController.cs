using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiSorteo.DTOS;
using WebApiSorteo.Seguridad;

namespace WebApiSorteo.Controllers
{
    [ApiController]
    [Route("CuentasCasino")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<UsuariosController> logger;
        public UsuariosController(UserManager<IdentityUser> userManager,
            IConfiguration configuration, SignInManager<IdentityUser> signInManager, ILogger<UsuariosController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.logger = logger;
        }
        [HttpPost("RegistroDeUsuarios")]
        public async Task<ActionResult<RespuestaDeAutenticacion>> Registrar(CredencialesDeUsuario credencialesUsuario)
        {
            logger.LogInformation("Entrada de datos");
            logger.LogInformation(credencialesUsuario.ToString());
            var user = new IdentityUser
            {
                UserName = credencialesUsuario.Nombre,
                Email = credencialesUsuario.email
            };
            logger.LogInformation("Creacion de la entidad de usuario");
            logger.LogInformation(user.ToString());
            var result = await userManager.CreateAsync(user, credencialesUsuario.password);
            if (result.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<RespuestaDeAutenticacion>> Login(CredencialesDeUsuario credencialesUsuario)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Nombre,
                credencialesUsuario.password, isPersistent: false, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }
        }
        [HttpPost("DeclaracionEmpleados")]
        public async Task<ActionResult> HacerAdmin(EditarCoordinadores editar)
        {
            var usuario = await userManager.FindByNameAsync(editar.Nombres);
            await userManager.AddClaimAsync(usuario, new Claim("AdminClaim", "1"));
            return NoContent();
        }
        [HttpPost("BajaDeEmpleados")]
        public async Task<ActionResult> QuitarAdmin(EditarCoordinadores editar)
        {
            var usuario = await userManager.FindByNameAsync(editar.Nombres);
            await userManager.RemoveClaimAsync(usuario, new Claim("AdminClaim", "1"));
            return NoContent();
        }

        [HttpGet("RenovarToken")]
        public async Task<ActionResult<RespuestaDeAutenticacion>> Renovar()
        {
            var nombreclaim = HttpContext.User.Claims.Where(claim => claim.Type == "Nombre").FirstOrDefault();
           
            System.Console.WriteLine($"<{nombreclaim}>");
            var nombre = nombreclaim.Value;
         
            var credenciales = new CredencialesDeUsuario()
            {
               
                Nombre = nombreclaim.Value,
            };

            return await ConstruirToken(credenciales);

        }
        private async Task<RespuestaDeAutenticacion> ConstruirToken(CredencialesDeUsuario credencialesUsuario)
        {
            var claims = new List<Claim>
            {
                new Claim("Nombre", credencialesUsuario.Nombre),
                new Claim("Email", credencialesUsuario.email)
            };
            var usuario = await userManager.FindByNameAsync(credencialesUsuario.Nombre);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var FechaExpiracion = DateTime.UtcNow.AddHours(1);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: FechaExpiracion, signingCredentials: creds);
            return new RespuestaDeAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                FechaCaducidad = FechaExpiracion
            };
        }

    }
}
