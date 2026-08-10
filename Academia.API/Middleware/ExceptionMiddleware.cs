using Academia.API.Errors;
using Academia.Domain.Validation;
using System.Net;
using System.Text.Json;

namespace Academia.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        private async Task HandleExceptionAsync(
           HttpContext context,
           Exception exception,
           HttpStatusCode statusCode)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var response = _env.IsDevelopment() ? new ApiException(context.Response.StatusCode.ToString(), exception.Message, exception.StackTrace) :
                new ApiException(context.Response.StatusCode.ToString(), exception.Message, "Internal server error");

            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, option);
            await context.Response.WriteAsync(json);

        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch(BadRequestException ex)
            {
                await HandleExceptionAsync(context,ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment() ? new ApiException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace) :
                    new ApiException(context.Response.StatusCode.ToString(), ex.Message, "Internal server error");

                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, option);
                await context.Response.WriteAsync(json);

            }
        }
    }
}
