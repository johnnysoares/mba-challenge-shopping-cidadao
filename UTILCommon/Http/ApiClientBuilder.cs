using System;
using System.Collections;
using Microsoft.Extensions.Logging;

namespace UTILCommon.Http {

    public class ApiClientBuilder : IApiClientBuilder {

        private Uri apiUri;
        private ApiClient restClient;
        private readonly ILogger<ApiClient> Logger;

        /// <summary>
        /// Construtor
        /// </summary>
        public ApiClientBuilder(ILogger<ApiClient> _Logger) {

            Logger = _Logger;
        }

        /// <summary>
        /// 
        /// </summary>
        public ApiClient build(string baseEndpoint, Hashtable headers) {

            this.apiUri = new Uri(baseEndpoint);

            this.restClient = new ApiClient(this.apiUri, headers, Logger);

            return this.restClient;
        }
    }
}
