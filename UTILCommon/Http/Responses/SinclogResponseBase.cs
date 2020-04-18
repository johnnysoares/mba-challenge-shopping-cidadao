using Newtonsoft.Json;
using System.Collections.Generic;

namespace UTILCommon.Http.Responses {

    public class SinclogResponseBase<T> where T : class {

        public bool flagErro { get; set; }

        public List<string> listaMensagens { get; set; }

        [JsonProperty(PropertyName = "info")]
        public T Info { get; set; }
    }
}

