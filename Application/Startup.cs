using System.Linq;
using Application.Data;
using Application.Errors;
using Application.Helpers;
using Application.Repositories;
using Application.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Application
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This will be called when running in development due to .NET Core's conventions.
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                x => x.UseNpgsql(_configuration.GetConnectionString("PostgreSqlConnection"),
                    x => x.SetPostgresVersion(10, 12)));

            ConfigureServices(services);
        }

        // This will be called when running in production due to .NET Core's conventions.
        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                x => x.UseNpgsql(_configuration.GetConnectionString("PostgreSqlConnection"),
                    x => x.SetPostgresVersion(10, 12)));

            ConfigureServices(services);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // This will initialize the mapping profile.
            services.AddAutoMapper(typeof(MappingProfiles));
            // This (SerializerSettings.ReferenceLoopHandling) will address the entities referencing themselves and prevent will prevent exceptions.
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            // This will initialize CaseRepository.
            services.AddScoped<ICaseRepository, CaseRepository>();

            // The following will configure the behavior of classes annotated with the ApiController attribute.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // The following event will occur when the model is in a state of error.
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    // The following will get the model state errors if any exist.
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    // The following will inject the model state errors into a ApiValidationErrorResponse object.
                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    // The following will return an http code 400 along with the model state errors.
                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // The following will handle exceptions depending on the current environment (develop, production, etc).
            app.UseMiddleware<ExceptionMiddleware>();

            // The following will handle routes that do not exist by redirecting the request to the a default route.
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
