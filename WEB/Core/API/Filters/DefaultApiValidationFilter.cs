using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WEB.Core.API.Models;

namespace WEB.Core.API.Filters {

    public class DefaultApiValidationFilter : ActionFilterAttribute{

        public override void OnActionExecuted(ActionExecutedContext context) {

            if (!context.ModelState.IsValid) {
                context.Result = new BadRequestObjectResult(new BaseApiResponse { status = 400, message = "O formato de dados enviado não é válido!" });
            }

            base.OnActionExecuted(context);
        }
    }
}
