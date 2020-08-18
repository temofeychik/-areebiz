using AutoMapper;
using CareebizExam.DbContext;
using CareebizExam.WebApi.Mappings;
using CareebizExam.WebApi.Services.GoogleMaps;
using CareebizExam.WebApi.Services.Pdf;
using CareebizExam.WebApi.Services.Rectangle;
using CareebizExam.WebApi.Services.Zip;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CareebizExam.WebApi
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
            services.AddDbContextPool<CareebizExamDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: action => action.EnableRetryOnFailure(
                                          maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Maps API", Version = "v1" });
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RectangleMappingProfile>();
                cfg.AddProfile<RectangleViewModelMappingProfile>();
            });

            IMapper mapper = new Mapper(config);

            services.AddSingleton(mapper);
            services.AddScoped<IRectangleService, RectangleService>();

            services.Configure<GoogleMapsOptions>(Configuration.GetSection("GoogleMapsApi"));
            services.AddScoped<IGoogleMapsService, GoogleMapsService>();

            services.AddSingleton<IPdfService, PdfService>();
            services.AddSingleton<IZipService, ZipService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(configuration =>
            {
                configuration.SwaggerEndpoint("/swagger/v1/swagger.json", "Maps API");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
