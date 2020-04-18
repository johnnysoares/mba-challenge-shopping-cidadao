using System.Collections.Generic;

namespace WEB.Core.API.Models {

    public class BaseApiResponse {

        public string message { get; set; }

        public string error { get; set; }

        public int status { get; set; }
    }
}
