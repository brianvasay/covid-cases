using Application.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly ILogger<ExceptionMiddleware> _logger;

        private readonly IHostEnvironment _environment;
        public ExceptionMiddleware(RequestDelegate request, ILogger<ExceptionMiddleware> logger,
            IHostEnvironment environment)
        {
            _environment = environment;
            _logger = logger;
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // This object processes the http request and if no exception is thrown, it moves on to the next middleware.
                await _request(context);
            }
            catch (Exception ex)
            {
                // The following will log the exception and exception message into the console.
                _logger.LogError(ex, ex.Message);
                // The following will format the error message that will be returned to the client.
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                // The following will create the error message for the client. 
                var response = _environment.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new ApiException((int)HttpStatusCode.InternalServerError);
                // The following will format the json response to camel case.
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                // The following will create the json response for the client.
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
