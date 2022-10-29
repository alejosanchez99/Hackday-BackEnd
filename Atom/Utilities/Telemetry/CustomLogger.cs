// <copyright file = "CustomLogger.cs" company="Intergrupo">
// Copyright(c) 2022 All Rights Reserved Intergrupo.
// </copyright>

namespace Atom.Utilities.Telemetry
{
    using System;
    using Microsoft.ApplicationInsights;

    /// <summary>
    /// Se encarga de guardar el seguimiento personalizado.
    /// </summary>
    public class CustomLogger : ICustomLogger
    {
        /// <summary>
        /// Obtiene o establece la telemetría.
        /// </summary>
        private readonly TelemetryClient telemetryClient;

        /// <summary>
        /// Se encarga de realizar la inyección de dependencia.
        /// </summary>
        /// <param name="telemetryClient">Telemetría.</param>
        public CustomLogger(TelemetryClient telemetryClient)
        {
            this.telemetryClient = telemetryClient;
        }

        /// <summary>
        /// Se encarga de insertar la traza.
        /// </summary>
        /// <param name="message">Contiene el mensaje de la traza.</param>
        public void InsertTrace(string message)
        {
            this.telemetryClient.TrackTrace(message);
            this.telemetryClient.Flush();
        }

        /// <summary>
        /// Se encarga en insertar la excepción.
        /// </summary>
        /// <param name="exception">Contiene la excepción.</param>
        public void InsertException(Exception exception)
        {
            this.telemetryClient.TrackException(exception);
            this.telemetryClient.Flush();
        }
    }
}
