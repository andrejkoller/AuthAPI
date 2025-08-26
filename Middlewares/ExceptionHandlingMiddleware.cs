namespace AuthAPI.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                if (ex is ArgumentException)
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                else if (ex is InvalidOperationException)
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                else if (ex is UnauthorizedAccessException)
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                var response = new
                {
                    error = ex.Message,
                    details = ex.InnerException?.Message ?? "An unexpected error occurred.",
                    statusCode = context.Response.StatusCode
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
