﻿using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RETO_CREDILIKEME.Controllers
{
    /// <summary>
    /// Clase para forzar uso de HTTPS en acciones usadas en api.
    /// Ver: http://www.asp.net/web-api/overview/security/working-with-ssl-in-web-api
    /// </summary>
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "HTTPS Required"
                };
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}