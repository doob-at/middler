using System.Collections;
using System.Collections.Generic;
using doob.middler.Common;
using doob.middler.Common.SharedModels.Models;
using doob.Reflectensions.ExtensionMethods;
using doob.Scripter.Shared;
using Microsoft.AspNetCore.Http;

namespace doob.middler.Action.Scripting.Models
{
    public class ScriptContextResponse
    {
        public int StatusCode
        {
            get => _middlerResponseContext.StatusCode;
            set => _middlerResponseContext.StatusCode = value;
        }
        public SimpleDictionary<string> Headers
        {
            get => _middlerResponseContext.Headers;
            set => _middlerResponseContext.Headers = value;
        }

        private readonly IMiddlerResponseContext _middlerResponseContext;
        private readonly IScriptEngine _scriptEngine;
        private readonly EndpointOptions _endpointOptions;

        public ScriptContextResponse(IMiddlerResponseContext middlerResponseContext, IScriptEngine scriptEngine,
            EndpointOptions endpointOptions)
        {
            _middlerResponseContext = middlerResponseContext;
            _scriptEngine = scriptEngine;
            _endpointOptions = endpointOptions;
        }
        
        public void SetBody(object? body = null)
        {
            if (body != null)
            {
                var isArray = body.GetType().IsEnumerableType();
                if (isArray)
                {
                    var list = new List<object>();
                    var arr = body as IEnumerable;
                    foreach (var o in arr!)
                    {
                        list.Add(o);
                    }

                    _middlerResponseContext.SetBody(list);
                    return;
                }
            }

            _middlerResponseContext.SetBody(body);
            
        }

        public void Send(int statusCode, object? body = null)
        {
            StatusCode = statusCode;
            SetBody(body);
            _endpointOptions.Terminating = true;
            _scriptEngine.Stop();
        }
        
        public void Ok(object? body = null)
        {
            Send(StatusCodes.Status200OK, body);
        }

        public void BadRequest(object? body = null)
        {
            Send(StatusCodes.Status400BadRequest, body);
        }

        public void NotFound(object? body = null)
        {
            Send(StatusCodes.Status404NotFound, body);
        }

        public void Redirect(string location, bool preserveMethod = true)
        {
            Headers["Location"] = location;
            var statusCode = preserveMethod ? StatusCodes.Status307TemporaryRedirect : StatusCodes.Status302Found;
            Send(statusCode);
        }

        public void RedirectPermanent(string location, bool preserveMethod = true)
        {
            Headers["Location"] = location;
            var statusCode = preserveMethod ? StatusCodes.Status308PermanentRedirect : StatusCodes.Status301MovedPermanently;
            Send(statusCode);
        }

        public void Unauthorized(object? body = null)
        {
            Send(StatusCodes.Status401Unauthorized, body);
        }

        public void Forbidden(object? body = null)
        {
            Send(StatusCodes.Status403Forbidden, body);
        }

    }
}