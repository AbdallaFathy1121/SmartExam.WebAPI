using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.ErorrHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Log the request
                Log.Information("Handling request: {Method} {Url}", context.Request.Method, context.Request.Path);

                // Call the next middleware in the pipeline
                await _next(context);

                // Log the response
                Log.Information("Finished handling request: {Method} {Url} with status code {StatusCode}",
                                context.Request.Method, context.Request.Path, context.Response.StatusCode);
            }
            catch (Exception ex)
            {
                // Log the exception
                Log.Error(ex, "An error occurred while handling request: {Method} {Url}",
                          context.Request.Method, context.Request.Path);
                throw;
            }
        }
    }
}
