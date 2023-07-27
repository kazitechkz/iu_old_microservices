using System.Net;
using System.Text.Json;
using OrganizationService.Application.Exceptions;

namespace OrganizationService.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log issues and handle exception response

            if (exception.GetType() == typeof(ValidationException))
            {

                var statusCode = HttpStatusCode.BadRequest;
                var response = new
                {
                    title = "Validation Error",
                    status = statusCode,
                    detail = exception.Message,
                    errors = GetErrors(exception)
                };
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            else
            {
                var code = HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize(new { isSuccess = false, error = exception.Message });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                await context.Response.WriteAsync(result);
            }
        }



        //Helpers

        private static IDictionary<string, string[]> GetErrors(Exception exception)
        {
            IDictionary<string, string[]> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors;
            }
            return errors;
        }
    }
}
