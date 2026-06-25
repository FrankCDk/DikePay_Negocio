using System.Net;
using System.Text.Json;
using DikePay.Shared.Models;

namespace DikePay.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode status;
            object response;

            // Diferenciamos el tipo de excepción
            switch (exception)
            {
                // Si usas FluentValidation o excepciones de negocio personalizadas:
                case FluentValidation.ValidationException validationEx:
                    status = HttpStatusCode.BadRequest;

                    var errors = validationEx.Errors.Select(e => new ApiError
                    {
                        Code = "VALIDATION_ERROR",
                        Message = e.ErrorMessage,
                        Field = e.PropertyName
                    }).ToList();

                    response = ApiResponse<object>.Fail("BAD_REQUEST", "Uno o más campos fallaron las validaciones.", errors);
                    break;


                case ConcurrencyException concurrencyException:
                    status = HttpStatusCode.Conflict;
                    response = ApiResponse<object>.Fail(concurrencyException.Code, concurrencyException.Message);
                    break;

                // Cualquier otra excepción inesperada (DB, NullReferenceException, etc.)
                default:
                    status = HttpStatusCode.InternalServerError;
                    response = ApiResponse<object>.Fail("SERVER_ERROR", exception.Message);
                    break;
            }

            context.Response.StatusCode = (int)status;

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}
