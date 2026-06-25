using System.Net;

namespace DikePay.Shared.Models
{
    public abstract class BaseException : Exception
    {
        public string Code { get; }
        public HttpStatusCode StatusCode { get; }

        protected BaseException(string code, string message, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(message)
        {
            Code = code;
            StatusCode = statusCode;
        }
    }

    // Excepción para Concurrencia (Conflicto 409)
    public class ConcurrencyException : BaseException
    {
        public ConcurrencyException(string message)
            : base("CONCURRENCY_CONFLICT", message, HttpStatusCode.Conflict) { }
    }

    // Excepción para Entidad no encontrada (404)
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message)
            : base("NOT_FOUND", message, HttpStatusCode.NotFound) { }
    }
}
