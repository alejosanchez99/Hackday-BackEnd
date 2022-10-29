// <copyright file = "ICustomLogger.cs" company="Intergrupo">
// Copyright(c) 2022 All Rights Reserved Intergrupo.
// </copyright>

namespace Atom.Utilities.Telemetry
{
    using System;

    /// <summary>
    /// Se encarga de guardar el seguimiento personalizado.
    /// </summary>
    public interface ICustomLogger
    {
        /// <summary>
        /// Se encarga de insertar la traza.
        /// </summary>
        /// <param name="message">Contiene el mensaje de la traza.</param>
        void InsertTrace(string message);

        /// <summary>
        /// Se encarga en insertar la excepción.
        /// </summary>
        /// <param name="exception">Contiene la excepción.</param>
        void InsertException(Exception exception);
    }
}
