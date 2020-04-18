using System.Collections.Generic;

namespace WEB._Core.Responses {

    public class BadRequestApi: BaseResponseApi {

        /// <summary>
        /// Construtor
        /// </summary>
        public BadRequestApi() {
            this.status = "400";
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public BadRequestApi(string _mensagem, string _erro, List<string> _listaErros) : this() {
            
            this.mensagem = _mensagem;
            
            this.error = _erro;
            
            this.listaErros = _listaErros;
        }
    }
}
