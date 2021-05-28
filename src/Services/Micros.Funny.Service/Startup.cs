using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Micros.Funny.Service.Providers;
using Micros.Funny.Service.Services;
using Microsoft.OpenApi.Any;

namespace Micros.Funny.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddJsonOptions(options => 
                    options.JsonSerializerOptions.PropertyNamingPolicy = null /* For PascalCase properties */
                    );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Micros.Funny.Service", Version = "v1" });
            });

            AddServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Micros.Funny.Service v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddServices(IServiceCollection services)
        {
            var animalType = Environment.GetEnvironmentVariable("ANIMAL_TYPE");
            switch (animalType)
            {
                case "dog":
                    services.AddScoped<IFactProvider, DogsFactProvider>();
                    services.AddScoped<IImageProvider, DogsImageProvider>();
                    break;
                case "cat":
                    services.AddScoped<IFactProvider, CatsFactProvider>();
                    services.AddScoped<IImageProvider, CatsImageProvider>();
                    break;
            }

            services.AddScoped<IFactService, FactService>();
            services.AddScoped<IImageService, ImageService>();
        }
    }
}
