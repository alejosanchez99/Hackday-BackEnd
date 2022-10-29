namespace Atom.Socket.Controllers
{
    using Atom.Socket.Hubs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    [Route("api/[controller]")]
    [ApiController]
    public class UserHubController : ControllerBase
    {

        /// <summary>
        /// Contiene el hub de la notificación. 
        /// </summary>
        private readonly IHubContext<UserHub> userHub;

        private readonly ILogger<UserHubController> logger;

        /// <summary>
        /// Constructor que realiza la inyección.
        /// </summary>
        /// <param name="userHub">Contiene la notificación.</param>
        /// <param name="logger">Contiene el log.</param>
        public UserHubController(IHubContext<UserHub> userHub, ILogger<UserHubController> logger)
        {
            this.userHub = userHub;
            this.logger = logger;
        }

        /// <summary>
        /// Api para actualizar el tablero de tareas programadas de envalsa
        /// </summary>
        /// <param name="nameListener">Objeto actualizaco de tareas programadas</param>
        /// <returns>Request Completed cuando la accion en Status200OK</returns>
        [HttpPost("{nameListener}")]
        public async Task<IActionResult> PostAsync(string nameListener)
        {
            IActionResult actionResult;

            try
            {
                await this.userHub.Clients.All.SendAsync(nameListener, new UserHub { Name = "prueba"  });

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
