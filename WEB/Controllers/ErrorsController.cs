using Microsoft.AspNetCore.Mvc;
using WEB.Core.API.Controllers;
using WEB.Core.API.Models;

namespace WEB.Controllers{
    
    [Route("errors/")]
    public class ErrorsController : BaseApiController {
        
        // GET api/values
        [Route("{code}")]
        public IActionResult error(int code){

            var Retorno = new BaseApiResponse {
                status = code, 
                message = "O recurso informado não pôde ser localizado"
            };

            return StatusCode(code, Retorno);
        }
    }
}
