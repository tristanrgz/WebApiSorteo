
namespace WebApiSorteo.Servicios
{
    public class ComunicacionConGanadores : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private Timer timer;

        public ComunicacionConGanadores(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            SalidaDeDatos("Inicio de Servicios", "Ejecuciones.txt");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            SalidaDeDatos("Servicio ejecutandose: Dia: " + DateTime.Now.ToString("dd/MM/yyyy "), "Ejecuciones.txt");
        }
        public void SalidaDeDatos(string msg, string documentodestino)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{documentodestino}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg); }
        }
    }
}
