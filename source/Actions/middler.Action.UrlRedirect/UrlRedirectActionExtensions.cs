using System;
using doob.middler.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace doob.middler.Action.UrlRedirect
{
    public static class UrlRedirectActionExtensions
    {
        
        public static IMiddlerOptionsBuilder AddUrlRedirectAction(this IMiddlerOptionsBuilder optionsBuilder, string alias = null)
        {
            alias = !String.IsNullOrWhiteSpace(alias) ? alias : UrlRedirectAction.DefaultActionType;
            optionsBuilder.ServiceCollection.AddTransient<UrlRedirectAction>();
            optionsBuilder.RegisterAction<UrlRedirectAction>(alias);

            return optionsBuilder;
        }
    }
}
