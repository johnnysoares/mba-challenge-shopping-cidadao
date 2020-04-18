using Microsoft.Extensions.Configuration;
using System.Collections;

namespace UTILCommon.Http {

    public class ApiConsumerBase {

        //Dependencias
        protected IApiClientBuilder ApiBuilder;
        protected string baseEndpointApi { get; set; }
        protected string routeEndpointApi { get; set; }
        protected string fullEndpointApi { get; set; }
        protected Hashtable headers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApiConsumerBase(IApiClientBuilder _ApiBuilder) {

            ApiBuilder = _ApiBuilder;

            headers = new Hashtable();
        }

        protected virtual string carregarFullEndpoint() {

            this.fullEndpointApi = $"{this.baseEndpointApi}{routeEndpointApi}";

            return this.fullEndpointApi;
        } 



    }
}
