using System.Collections.Generic;
using Newtonsoft.Json;

namespace WEB._Core.Responses {
    public class BaseResponseApi {

        [JsonProperty(PropertyName = "status")]
        public string status { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string mensagem { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string error { get; set; }
        
        [JsonProperty(PropertyName = "cause")]
        public List<string> listaErros { get; set; }
    }
}
