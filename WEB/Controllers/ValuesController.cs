using Microsoft.AspNetCore.Mvc;
using WEB.Core.API.Controllers;
using WEB.Core.API.Models;

namespace WEB.Controllers{
    
    [Route("api/values/")]
    public class ValuesController : BaseApiController {
        
        // GET api/values
        public IActionResult get(){

            var Retorno = new BaseApiResponse {
                status = 200, 
                message = "API iniciada com sucesso"
            };

            return StatusCode(200, Retorno);
        }
    }
}
