using System;
using System.Threading.Tasks;
using doob.middler.Action.Scripting.Models;
using doob.middler.Common;
using doob.middler.Common.SharedModels.Models;
using doob.Scripter.Shared;
using NamedServices.Microsoft.Extensions.DependencyInjection;

namespace doob.middler.Action.Scripting
{
    public class ScriptingAction: MiddlerAction<ScriptingOptions>
    {
        internal static string DefaultActionType => "Script";
        public override string ActionType => DefaultActionType;

        public override bool Terminating { get; set; } = true;

        private readonly IServiceProvider _serviceProvider;

        private IScriptEngine scriptEngine { get; set; }
        public ScriptingAction(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteRequestAsync(IMiddlerContext middlerContext)
        {
           
            scriptEngine = _serviceProvider.GetRequiredNamedService<IScriptEngine>(Parameters.Language);

            var compile = scriptEngine.NeedsCompiledScript && (!string.IsNullOrEmpty(Parameters.SourceCode) &&
                                                               string.IsNullOrWhiteSpace(Parameters.CompiledCode));

            
            if (compile)
            {
                CompileScriptIfNeeded();
            }

            scriptEngine.AddModuleParameterInstance(
                typeof(EndpointModuleInitializeParameters), 
                () => new EndpointModuleInitializeParameters(middlerContext, scriptEngine, Terminating)
                );

            scriptEngine.AddTaggedModules("EndpointRule");
            try
            {
                await scriptEngine.ExecuteAsync(scriptEngine.NeedsCompiledScript ? Parameters.CompiledCode! : Parameters.SourceCode!);
                //SendResponse(middlerContext.Response, scriptContext);
            }
            catch
            {
                throw;
                //await httpContext.BadRequest(e.GetBaseException().Message);
            }

            var endpointModule = scriptEngine.GetModuleState<EndpointModule>();

            Terminating = endpointModule?.Options.Terminating ?? Terminating;
        }


        public void ExecuteResponseAsync()
        {
            var responseFunction = scriptEngine.GetFunction("ExecuteResponse");
            if (responseFunction != null)
            {
                responseFunction.Invoke();
            }
        }

        public string? CompileScriptIfNeeded()
        {
            
            IScriptEngine scriptEngine = _serviceProvider.GetRequiredNamedService<IScriptEngine>(Parameters.Language);
            if (scriptEngine.NeedsCompiledScript)
            {
                Parameters.CompiledCode = scriptEngine.CompileScript.Invoke(Parameters.SourceCode ?? "");
            }

            return Parameters.CompiledCode;
        }


    }
}
