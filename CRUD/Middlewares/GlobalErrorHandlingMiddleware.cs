using CRUD.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace CRUD.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger <GlobalErrorHandlingMiddleware> logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next,ILogger<GlobalErrorHandlingMiddleware>_logger)
        {
            _next = next;
            logger = _logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                await  _next(context);
                stopwatch.Stop();
                logger.LogInformation($"Requst { context.Request.Path} Toke {stopwatch.ElapsedMilliseconds}");


          
            }

            catch (Exception ex)
            {
                var problem = new ProblemDetails
                {
                    Title = ex.Message,
                    Detail = ex?.InnerException?.Message,
                };

                if (ex is AppNotFoundException)
                {
                    problem.Status = (int)HttpStatusCode.NotFound;
                }

                else if (ex is AppBadRequestException)
                {
                    problem.Status = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(problem);

                }

                else
                {
                    problem.Status = (int)HttpStatusCode.InternalServerError;
                }

                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
