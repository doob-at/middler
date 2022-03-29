using System;
using doob.middler.Common;
using doob.middler.Common.Interfaces;
using doob.middler.Common.SharedModels.Models;
using Scriban;

namespace doob.middler.Action.UrlRewrite
{
    public class UrlRewriteAction: MiddlerAction<UrlRewriteOptions>
    {
        internal static string DefaultActionType => "UrlRewrite";

        public override bool Terminating => false;

        public override bool WriteStreamDirect => false;
        public override string ActionType => DefaultActionType;


        public void ExecuteRequest(IMiddlerContext middlerContext, IActionHelper actionHelper)
        {

            var template = Template.Parse(Parameters.RewriteTo);
            var rewriteTo = template.Render(middlerContext.Request);

            var isAbsolute = Uri.IsWellFormedUriString(rewriteTo, UriKind.Absolute);
            if (isAbsolute)
            {
                middlerContext.Request.Uri = new Uri(rewriteTo);
            }
            else
            {
                var builder = new UriBuilder(middlerContext.Request.Uri);
                builder.Query = null;
                if (rewriteTo.Contains("?"))
                {
                    builder.Path = rewriteTo.Split("?")[0];
                    builder.Query = rewriteTo.Split("?")[1];
                }
                else
                {
                    builder.Path = rewriteTo;
                }
                
                
                middlerContext.Request.Uri = builder.Uri;
            }


        }

        public void ExecuteResponse(IMiddlerContext middlerContext)
        {
            
           
        }
    }
}
