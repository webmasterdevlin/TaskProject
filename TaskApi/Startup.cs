using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NSwag.AspNetCore;
using TaskApi.Contracts;
using TaskApi.Data;
using AutoMapper;
using TaskApi.Dtos;
using TaskApi.Entities;
using TaskApi.Helpers;
using TaskApi.Repositories;

namespace TaskApi
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
            services.AddScoped<ITaskRepository, TaskRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DataContext")));

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Logging
            loggerFactory.AddConsole();
            loggerFactory.AddDebug(LogLevel.Information);
            //            loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            loggerFactory.AddNLog(); // shortcut of the above

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Please try again later");
                    });
                });
            }

            // Enable the Swagger UI middleware and the Swagger generator
            //            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            //            {
            //                settings.GeneratorSettings.DefaultPropertyNameHandling =
            //                    PropertyNameHandling.CamelCase;
            //            });

            // Register the Swagger generator
            app.UseSwaggerUi(typeof(Startup).Assembly, settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Task API";
                    document.Info.Description = "TaskProject ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Juan Dela Cruz",
                        Email = "jdc@gmail.com",
                        Url = "https://twitter.com/jdc"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });

            // AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TaskEntity, TaskDto>().ForMember(dest => dest.DaysRemaining,
                    opt => opt.MapFrom(src => src.DeadLine.GetDeadLine()));
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}