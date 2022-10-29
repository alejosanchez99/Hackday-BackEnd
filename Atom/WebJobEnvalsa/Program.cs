// <copyright file="Program.cs" company="Intergrupo">
// Copyright(c) 2020 All Rights Reserved Intergrupo.
// Proyecto: Framework Base Intergrupo
// </copyright>
// <author>Santiago Gil Roldán</author>
// <email>mailto:sgil@intergrupo.com</email>
// <summary></summary>
namespace WebJobAtom
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Se encarga de ejecutar el webjob.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        protected Program() { }

        /// <summary>
        /// Se encarga de instanciar todas las interfaces  y la instancia de base de datos y posteriormente ejecutar el webjob.
        /// </summary>
        private static async Task Main()
        {
            IHost host = GetHostBuilder();

            using (host)
            {
                await host.RunAsync();
            }
        }

        /// <summary>
        /// Contiene la configuración del host builder.
        /// </summary>
        /// <returns>Interfaz de host.</returns>
        private static IHost GetHostBuilder()
        {
            return new HostBuilder()
                .UseEnvironment("development")
                .ConfigureHostConfiguration(configurationBuilder => ConfigureHostConfigurationBuilder(configurationBuilder))
                .ConfigureAppConfiguration(configurationBuilder => ConfigureAppConfigurationBuilder(configurationBuilder))
                .ConfigureLogging((hostContext, configLogging) => ConfigureLogging(configLogging, hostContext.Configuration))
                .ConfigureServices((hostContext, services) => ConfigureServices(services, hostContext.Configuration))
                .Build();
        }

        /// <summary>
        /// Se encarga de configurar el logging.
        /// </summary>
        /// <param name="configLogging">Contiene la configuración de logging.</param>
        /// <param name="configuration">Contiene la configuración.</param>
        private static void ConfigureLogging(ILoggingBuilder configLogging, IConfiguration configuration)
        {
            configLogging.SetMinimumLevel(LogLevel.Trace);
            configLogging.AddApplicationInsights(configuration["ApplicationInsights:InstrumentationKey"]);
        }

        /// <summary>
        /// Se encarga de realizar la configuración de los servicios.
        /// </summary>
        /// <param name="services">Contiene la configuración de los servicios.</param>
        /// <param name="configuration">Contiene la configuración.</param>
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<ICustomLogger, CustomLogger>();

            string instrumentationKey = configuration["ApplicationInsights:InstrumentationKey"];

            services.AddHostedService<ApplicationHostService>();
            services.AddMemoryCache();
        }

        /// <summary>
        /// Se encarga de configurar el host.
        /// </summary>
        /// <param name="configurationBuilder">Contiene la configuración del builder.</param>
        private static void ConfigureHostConfigurationBuilder(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName);
        }

        /// <summary>
        /// Se encarga de configurar el builder.
        /// </summary>
        /// <param name="configurationBuilder">Contiene la configuración del builder.</param>
        /// <param name="environmentName">Contiene el nombre del ambiente.</param>
        private static void ConfigureAppConfigurationBuilder(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddJsonFile("appsettings.json", false);
        }
    }
}