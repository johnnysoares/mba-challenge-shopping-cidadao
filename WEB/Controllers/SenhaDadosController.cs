using System.Threading.Tasks;
using BLL.Atendimentos.Services;
using BLL.Pracas.ConstEnums;
using Microsoft.AspNetCore.Mvc;
using WEB.Core.API.Controllers;
using WEB.Core.API.Models;

namespace WEB.Controllers{
    
    [Route("api/senha-dados/")]
    public class SenhaDadosController : BaseApiController {

        private readonly IBuscadorDados buscadorDados;

        /// <summary>
        /// Construtor
        /// </summary>
        public SenhaDadosController(IBuscadorDados _buscadorDados) {
            buscadorDados = _buscadorDados;
        }
        
        // GET api/atividades
        public async Task<IActionResult> get() {

            await this.buscadorDados.migrarCE(new PracaConst());
            
            var Retorno = new BaseApiResponse {
                status = 200, 
                message = "Rotina de busca de informações dos atendimentos"
            };

            return StatusCode(200, Retorno);
        }
    }
}
