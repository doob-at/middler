﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading;
using doob.middler.Common.SharedModels.Models;
using doob.Reflectensions;

namespace doob.middler.Common
{
    public interface IMiddlerRequestContext
    {
        
        ClaimsPrincipal Principal { get; set; }
        IPAddress? SourceIPAddress { get; set; }
        List<IPAddress> ProxyServers { get; set; }
        MiddlerRouteData RouteData { get; set; }
        ExpandableObject Headers { get; set; }
        MiddlerRouteQueryParameters QueryParameters { get; set; }
        Uri Uri { get; set; }
        
        string HttpMethod { get; set; }
        CancellationToken RequestAborted { get; }

        string ContentType { get; set; }

        string GetBodyAsString();

        void SetBody(object body);
    }
}
