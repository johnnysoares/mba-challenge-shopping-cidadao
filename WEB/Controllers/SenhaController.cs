using System.Threading.Tasks;
using BLL.Atendimentos.Services;
using BLL.Pracas.ConstEnums;
using Microsoft.AspNetCore.Mvc;
using WEB.Core.API.Controllers;
using WEB.Core.API.Models;

namespace WEB.Controllers{
    
    [Route("api/senha/")]
    public class SenhaController : BaseApiController {

        private readonly IBuscadorSenha buscadorSenha;

        /// <summary>
        /// Construtor
        /// </summary>
        public SenhaController(IBuscadorSenha _buscadorSenha) {
            buscadorSenha = _buscadorSenha;
        }
        
        // GET api/atividades
        public async Task<IActionResult> get() {

            await this.buscadorSenha.migrarRJ(new PracaConst());
            
            var Retorno = new BaseApiResponse {
                status = 200, 
                message = "Rotina de busca dos atendimentos para unificaçao das senhas"
            };

            return StatusCode(200, Retorno);
        }
    }
}
