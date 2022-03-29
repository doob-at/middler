using System;
using doob.middler.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace doob.middler.Action.UrlRewrite
{
    public static class UrlRewriteActionExtensions
    {
        public static IMiddlerOptionsBuilder AddUrlRewriteAction(this IMiddlerOptionsBuilder optionsBuilder, string alias = null)
        {
            alias = !String.IsNullOrWhiteSpace(alias) ? alias : UrlRewriteAction.DefaultActionType;
            optionsBuilder.ServiceCollection.AddTransient<UrlRewriteAction>();
            optionsBuilder.RegisterAction<UrlRewriteAction>(alias);

            return optionsBuilder;
        }
        
    }
}