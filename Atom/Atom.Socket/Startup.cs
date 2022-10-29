namespace Atom.Socket
{
    using Atom.Socket.Hubs;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public partial class Startup
    {
        protected IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string[] cors = new string[] { "https://hackdaykonecta.z13.web.core.windows.net", "http://192.168.56.1:3000", "http://localhost:3000", "https://localhost:3000" };

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins(cors)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            services.AddResponseCompression();

            services.AddSignalR();

            services.AddControllers()
                    .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true)
                    .AddJsonOptions(options => options.JsonSerializerOptions.MaxDepth = int.MaxValue);

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();
            app.UseResponseCompression();
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<UserHub>("/hubs/user");

                endpoints.MapHealthChecks("/healh", new HealthCheckOptions
                {
                    AllowCachingResponses = false
                });
            });
        }
    }
}