using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MaxiTransfers.Api.Servicios;
using NSwag;

namespace MaxiTransfers.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            //Trace.AutoFlush = true;
            //Trace.Indent();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IEmpleadosService, EmpleadosService>();
            services.AddScoped<IBeneficiariosService, BeneficiariosService>();
            services.AddSwaggerDocument(c =>
            {
                c.Title = "Servicio Maxitransfer";
                c.Description = "servicio que contiene los CRUD de empleados y beneficiarios desde la cual se\r\nrealizarán todas las acciones";
                c.Version = "v2";
            });

            services.AddDbContext<PersistenceContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Connstring"));
            });

            services.AddControllers(mvcOpts =>
            {
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

        }


        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

        }
    }
}
