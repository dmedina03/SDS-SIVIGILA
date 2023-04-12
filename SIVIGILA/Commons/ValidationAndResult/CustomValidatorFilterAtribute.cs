using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.Utils.ExtenssionMethods;

namespace SIVIGILA.Commons.ValidationAndResult
{
    /// <summary>
    /// Sobreescritura del valor por defecto al momento de vlaidar la estructura del objeto, para adecuarse al tipo de respuesta que se implemento para el sistema
    /// </summary>
    public class CustomValidationFilterAttributecs : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            ResponseDTO<Dictionary<string, object>> response = new()
            {
                Status = 422,
                Message = "One or more problems occurred while processing your request",
                Title = "submitted information is in an incorrect format",
                Data = context.ModelState.GetErrorsToDictionary()
            };

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(response);
            }

            base.OnActionExecuting(context);
        }
    }
}
