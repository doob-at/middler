using System;
using doob.middler.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace doob.middler.Action.Scripting
{
    public static class ScriptingActionExtensions
    {
        
        public static IMiddlerOptionsBuilder AddScriptingAction(this IMiddlerOptionsBuilder optionsBuilder, string? alias = null)
        {
            alias = !String.IsNullOrWhiteSpace(alias) ? alias : ScriptingAction.DefaultActionType;
            optionsBuilder.ServiceCollection.AddTransient<ScriptingAction>();
            optionsBuilder.RegisterAction<ScriptingAction>(alias);
            
            return optionsBuilder;
        }
    }
}
