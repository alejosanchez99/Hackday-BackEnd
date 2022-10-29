// <copyright file="ApplicationHostService.cs" company="Intergrupo">
// Copyright(c) 2022 All Rights Reserved Intergrupo.
// </copyright>
namespace WebJobAtom
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Atom.Business;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Se encarga de administrar tareas asincronas para ejecutar en el host.
    /// </summary>
    internal class ApplicationHostService : IHostedService
    {

        /// <summary>
        /// Instancia de la vida de la aplicación.
        /// </summary>
        private readonly IApplicationLifetime applicationLifetime;

        /// <summary>
        /// Se encarga de realizar la inyección de dependencias.
        /// </summary>
        /// <param name="customLogger">Contiene el log personalizado.</param>
        /// <param name="applicationLifetime">Contiene la vida de la aplicación.</param>
        public ApplicationHostService(IApplicationLifetime applicationLifetime)
        {
            this.applicationLifetime = applicationLifetime;
        }

        /// <summary>
        /// Se encarga de inicializar la tarea asincrona.
        /// </summary>
        /// <param name="cancellationToken">Cancelación del token.</param>
        /// <returns>Tarea asincrona.</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                KonectaAdapter konectaAdapter = new KonectaAdapter();

                konectaAdapter.Processs();

                //await this.appProcessService.RunScheduledTaskAsync();
            }
            finally
            {
                applicationLifetime.StopApplication();
            }
        }

        /// <summary>
        /// Se encarga de detener la tarea asincrona.
        /// </summary>
        /// <param name="cancellationToken">Cancelación del token.</param>
        /// <returns>Tarea asincrona.</returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                //this.customLogger.InsertTrace("Se ha detenido el webjob");
            }
            catch (Exception exception)
            {
                //this.customLogger.InsertException(exception);
            }
            finally
            {
                await Task.CompletedTask;
            }
        }
    }
}