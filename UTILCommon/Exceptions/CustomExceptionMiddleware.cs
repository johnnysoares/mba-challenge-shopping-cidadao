using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UTILCommon.Http.Responses;

namespace UTILCommon.Exceptions {

    public class CustomExceptionMiddleware : IMiddleware {

        //Dependencias
        private readonly ILogger         Logger;
        private readonly IResponseHttpBase InfoResponse;
        
        /// <summary>
        /// 
        /// </summary>
        public CustomExceptionMiddleware(ILogger _logger,
                                         IResponseHttpBase _InfoResponse) {

            Logger = _logger;

            InfoResponse = _InfoResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
            
            try {
                
                await next(context);
                
            } catch (Exception ex) {
                
                Logger.LogError($"Erro de execução: {ex}");
                
                await HandleExceptionAsync(context, ex, this.InfoResponse);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IResponseHttpBase CustomResponse) {
            
            context.Response.ContentType = "application/json";
            
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            
            CustomResponse.status = "ERROR";
            
            CustomResponse.mensagem = "Internal server error";

            return context.Response.WriteAsync(JsonConvert.SerializeObject(CustomResponse));
        }

       

    }

}

