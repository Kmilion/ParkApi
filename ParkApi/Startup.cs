using DataAccess.Context;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ParkApi.Mappings;
using Services.Managers;
using Services.Managers.IManagers;
using Services.Mappings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ParkApi
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
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<INationalParksRepository, NationalParksRepository>();
            services.AddScoped<ITrailsRepository, TrailsRepository>();
            services.AddScoped<INationalParksManager, NationalParksManager>();
            services.AddScoped<ITrailsManager, TrailsManager>();

            services.AddAutoMapper(typeof(DTOToEntity));
            services.AddAutoMapper(typeof(EntityToDTO));
            services.AddAutoMapper(typeof(DTOToVM));
            services.AddAutoMapper(typeof(VMToDTO));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ParkOpenAPISpecNationalParks",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "ParkApi - National Parks",
                        Version = "v1",
                        Description = "National Parks Api",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "gonzaloeceballos@gmail.com",
                            Name = "Gonzalo Ceballos",
                            Url = new Uri("https://github.com/Kmilion")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://es.wikipedia.org/wiki/Licencia_MIT")
                        }
                    });

                options.SwaggerDoc("ParkOpenAPISpecTrails",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "ParkApi - Trails",
                        Version = "v1",
                        Description = "Trails Api",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "gonzaloeceballos@gmail.com",
                            Name = "Gonzalo Ceballos",
                            Url = new Uri("https://github.com/Kmilion")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://es.wikipedia.org/wiki/Licencia_MIT")
                        }
                    });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/ParkOpenAPISpecNationalParks/swagger.json", "ParkApi - National Parks");
                options.SwaggerEndpoint("/swagger/ParkOpenAPISpecTrails/swagger.json", "ParkApi - Trails");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
