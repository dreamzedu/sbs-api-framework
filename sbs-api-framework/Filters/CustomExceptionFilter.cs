using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Filters;
using Microsoft.Ajax.Utilities;
using sbs_api.models;

namespace sbs_api_framework.Filters
{
    
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
      
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string error = string.Empty;
            HttpResponseMessage response;

            if (actionExecutedContext.Exception is ValidationException)
            {
                 error = "validation:" +((ValidationException)actionExecutedContext.Exception).ValidationMessage;
            }
            else
            {
                error = "error:server_error";
            }

            response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed)
            {
                Content = new StringContent(error),
                ReasonPhrase = "Internal Server Error.",
                StatusCode = HttpStatusCode.InternalServerError
            };

            actionExecutedContext.Response = response;
        }
    }
}