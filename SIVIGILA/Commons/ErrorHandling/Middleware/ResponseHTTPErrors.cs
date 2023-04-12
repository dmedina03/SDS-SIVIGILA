using Newtonsoft.Json;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using System.Net;

namespace SIVIGILA.Commons.ErrorHandling.Middleware
{
    public static class ResponseHTTPErrors
    {
        public static string Set400(UnfinishedOperationException exception)
        {
            ResponseMiddleWareErrorDto<string> result = new()
            {
                status = (int)HttpStatusCode.BadRequest,
                title = "The operation could not be completed",
                message = exception.Message,
                data = exception.Message
            };
            return SerializaResponse(result);
        }

        public static string Set404(NotFoundException exception)
        {
            ResponseMiddleWareErrorDto<string> result = new()
            {
                status = (int)HttpStatusCode.NotFound,
                title = "Not Found",
                message = exception.Message,
                data = exception.Message
            };
            return SerializaResponse(result);
        }

        public static string Set422(ValidationDataException exception)
        {
            ResponseMiddleWareErrorDto<IDictionary<string, string[]>> result = new()
            {
                status = (int)HttpStatusCode.UnprocessableEntity,
                title = "One or more validation problems were found in the model",
                message = exception.Message,
                data = exception.Errores
            };
            return SerializaResponse(result);
        }

        public static string Set500(Exception exception)
        {
            ResponseMiddleWareErrorDto<string> result = new()
            {
                status = (int)HttpStatusCode.InternalServerError,
                title = "An unexpedted exception ocurred",
                message = exception.Message,
                data = exception.Message
            };
            return SerializaResponse(result);
        }

        private static string SerializaResponse(object result)
        {
            return JsonConvert.SerializeObject(result);
        }

        internal record ResponseMiddleWareErrorDto<T>
        {
            public int status { get; set; }
            public string message { get; set; }
            public string title { get; set; }
            public T data { get; set; }
        }
    }
}
