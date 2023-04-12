using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using System.Net;

namespace SIVIGILA.Commons.ErrorHandling.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(NotFoundException nfe)
            {
                var data = ResponseHTTPErrors.Set404(nfe);
                await HandleExceptionAsync(context, HttpStatusCode.OK, data);
            }
            catch(ValidationDataException vde)
            {
                var data = ResponseHTTPErrors.Set422(vde);
                await HandleExceptionAsync(context, HttpStatusCode.UnprocessableEntity, data);
            }
            catch(UnfinishedOperationException uope)
            {
                var data = ResponseHTTPErrors.Set400(uope);
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, data);
            }
            catch(Exception ex)
            {
                var data = ResponseHTTPErrors.Set500(ex);
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, data);
            }

        }

        /// <summary>
        /// Encapsula la excepcion en un response de tipo problem+json
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string error)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(error);
        }
    }
}
