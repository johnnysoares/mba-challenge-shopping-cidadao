using Newtonsoft.Json;
using System;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UTILCommon.Extensions;
using UTILCommon.Models;

namespace UTILCommon.Http {

    public class ApiClient {

        //Dependencies
        private readonly HttpClient httpClient;
        private Uri BaseEndpoint { get; set; }
        private Hashtable headers { get; set; }

        private readonly ILogger<ApiClient> Logger;

        /// <summary>
        /// Construtor
        /// </summary>
        public ApiClient(Uri baseEndpoint, 
                         Hashtable _headers,
                         ILogger<ApiClient> _Logger) {

            if (baseEndpoint == null) {
                throw new ArgumentNullException("baseEndpoint");
            }

            BaseEndpoint = baseEndpoint;

            headers = _headers;

            Logger = _Logger;

            httpClient = new HttpClient();
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task<ResponseHttp<T>> readResponse<T>(HttpResponseMessage response) {

            var Retorno = new ResponseHttp<T>();

            Retorno.code = response.StatusCode;

            Retorno.body = await response.Content.ReadAsStringAsync();

            this.Logger.LogInformation($"INFO Retorno de {response.RequestMessage?.RequestUri}: {Retorno.body}");

            if ((int) Retorno.code >= 300) {
                
                this.Logger.LogWarning($"WARNING Retorno de {response.RequestMessage?.RequestUri}: {Retorno.body}");

                Retorno.fail = true;

                if(typeof(T) != typeof(string)) {
                    Retorno.Entity = JsonConvert.DeserializeObject<T>(Retorno.body);
                }

                return Retorno;
            }

            if (response.Content.Headers.ContentType.MediaType.Contains("text/html")) {

                Retorno.isHtml = true;

            }

            if (response.Content.Headers.ContentType.MediaType.Contains("application/json")) {

                Retorno.isJSON = true;

                if(typeof(T) != typeof(string)) {
                    Retorno.Entity = JsonConvert.DeserializeObject<T>(Retorno.body);
                }
            }


            return Retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<ResponseHttp<T>> GetAsync<T>() {

            ResponseHttp<T> Result = new ResponseHttp<T>();

            addHeaders();

            var response = await httpClient.GetAsync(BaseEndpoint, HttpCompletionOption.ResponseHeadersRead);

            Result = await readResponse<T>(response);

            return Result;
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        public async Task<ResponseHttp<T>> PostAsync<T>(T content) {

            addHeaders();

            var url = this.BaseEndpoint.ToString();

            HttpContent postData = null;

            if (content != null) {
                
                postData = createHttpContent<T>(content);    
            }

            var response = await httpClient.PostAsync(url, postData);

            return await readResponse<T>(response);
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<ResponseHttp<T1>> PostAsync<T1, T2>(T2 content) {

            addHeaders();

            var response = await httpClient.PostAsync(BaseEndpoint, createHttpContent<T2>(content));

            return await readResponse<T1>(response);
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<ResponseHttp<T1>> PutAsync<T1, T2>(T2 content) {

            addHeaders();

            var response = await httpClient.PutAsync(BaseEndpoint, createHttpContent<T2>(content));

            return await readResponse<T1>(response);
        }

        /// <summary>
        /// 
        /// </summary>
        private Uri CreateRequestUri(string relativePath, string queryString = "") {

            var endpoint = new Uri(BaseEndpoint, relativePath);

            var uriBuilder = new UriBuilder(endpoint);

            uriBuilder.Query = queryString;

            return uriBuilder.Uri;
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpContent createHttpContent<T>(T content) {

            string json;
            
            if(!content.Is<string>()) {

                json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);

            } else {

                json = content as string;
            }

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// 
        /// </summary>
        public static JsonSerializerSettings MicrosoftDateFormatSettings {
            get {
                return new JsonSerializerSettings {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpRequestHeaders addHeaders() {

            foreach (var key in this.headers.Keys) {

                string nameHeader = key.stringOrEmpty();
                string valueHeader = headers[key].stringOrEmpty();

                if (nameHeader.isEmpty()) {
                    continue;
                }

                httpClient.DefaultRequestHeaders.Add(nameHeader, valueHeader);
            }

            return httpClient.DefaultRequestHeaders;
        }
    }

}
