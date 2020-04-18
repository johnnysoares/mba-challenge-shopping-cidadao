using System.Net;

namespace UTILCommon.Models {

    public class ResponseHttp<T> {

        //public string Token { get; set; }
        //public Boolean IsAuthenicated;
        public HttpStatusCode code;
        public string body { get; set; }
        public bool isHtml { get; set; }
        public bool isJSON { get; set; }
        public bool fail { get; set; }
        public T Entity;

        /// <summary>
        /// Construtor
        /// </summary>
        public ResponseHttp() {

        }

        /// <summary>
        /// 
        /// </summary>
        public ResponseHttp(string _body) : this() {

            this.body = _body;

        }

    }
}
