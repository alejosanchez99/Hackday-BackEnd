namespace Atom.Socket.Controllers
{
    using Atom.Entities;
    using Atom.Socket.Hubs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    [Route("api/[controller]")]
    [ApiController]
    public class MetricHubController : ControllerBase
    {
        private readonly IHubContext<MetricHub> metricHub;

        private readonly ILogger<MetricHubController> logger;

        public MetricHubController(IHubContext<MetricHub> metricHub, ILogger<MetricHubController> logger)
        {
            this.metricHub = metricHub;
            this.logger = logger;
        }

        /// <summary>
        /// Api para actualizar el tablero de tareas programadas de envalsa
        /// </summary>
        /// <param name="nameListener">Objeto actualizaco de tareas programadas</param>
        /// <returns>Request Completed cuando la accion en Status200OK</returns>
        [HttpPost("{nameListener}")]
        public async Task<IActionResult> PostAsync(string nameListener, [FromBody] Metric metric)
        {
            IActionResult actionResult;

            try
            {
                await this.metricHub.Clients.All.SendAsync(nameListener, new MetricHub { Metric = metric });

                actionResult = this.Ok(new { Message = "Request Completed" });
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, exception.Message);

                actionResult = this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return actionResult;
        }
    }
}
