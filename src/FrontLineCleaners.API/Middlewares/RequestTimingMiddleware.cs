
using System.Diagnostics;

namespace FrontLineCleaners.API.Middlewares
{
    public class RequestTimingMiddleware(ILogger<RequestTimingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var watch = Stopwatch.StartNew();
            await next.Invoke(context);
            watch.Stop();
            var timeTaken = watch.Elapsed.TotalSeconds;
            //check if the request execution total time is more than 4 secs
            if (timeTaken > 4)
            {
                logger.LogInformation($"Long running request: Method: {context.Request.Method} Path: {context.Request.Path} Time: {timeTaken}");
            }
        }
    }
}
