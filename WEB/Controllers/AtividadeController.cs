using System.Threading.Tasks;
using BLL.Atividades.Services;
using Microsoft.AspNetCore.Mvc;
using WEB.Core.API.Controllers;
using WEB.Core.API.Models;

namespace WEB.Controllers{
    
    [Route("api/atividades/")]
    public class AtividadesController : BaseApiController {

        private readonly IAtividadeMigracao migracaoAtividade;

        /// <summary>
        /// Construtor
        /// </summary>
        public AtividadesController(IAtividadeMigracao _migracaoAtividade) {
            migracaoAtividade = _migracaoAtividade;
        }
        
        // GET api/atividades
        public async Task<IActionResult> get() {

            await this.migracaoAtividade.migrarTudo();
            
            var Retorno = new BaseApiResponse {
                status = 200, 
                message = "Rotina de tratamento de atividades realizada com sucesso"
            };

            return StatusCode(200, Retorno);
        }
    }
}
