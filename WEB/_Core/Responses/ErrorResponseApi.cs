namespace WEB._Core.Responses {

    public class ErrorResponseApi:BaseResponseApi{

        /// <summary>
        /// Construtor
        /// </summary>
        public ErrorResponseApi() {
            this.status = "500";
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public ErrorResponseApi(string _mensagem, string _erro) : this() {
            
            this.mensagem = _mensagem;
            
            this.mensagem = _erro;
        }
    }
}
