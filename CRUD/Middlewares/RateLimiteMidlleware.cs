namespace CRUD.Middlewares
{
    public class RateLimiteMidlleware
    {
        private readonly RequestDelegate _next;
        private static int conter = 0;
        private static DateTime lastRequest = DateTime.Now;

        public RateLimiteMidlleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            conter++;
            if (DateTime.Now.Subtract(lastRequest).TotalSeconds > 10)
            {
                conter = 1;
                lastRequest = DateTime.Now;
                await _next(context);
            }

            if (conter > 5)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Rate limit Exceded");
                return;
            }
            await _next(context);


        }
    }

}
