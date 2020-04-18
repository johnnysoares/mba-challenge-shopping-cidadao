using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB.Core.API.Filters {

    public class DefaultApiFilter : Attribute, IResourceFilter {

        /// <summary>
        /// 
        /// </summary>
        public void OnResourceExecuting(ResourceExecutingContext context) {

        }

        public void OnResourceExecuted(ResourceExecutedContext context) {

        }
    }
}
