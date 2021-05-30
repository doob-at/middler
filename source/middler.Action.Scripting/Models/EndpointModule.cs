using doob.middler.Common;
using doob.middler.Common.SharedModels.Models;
using doob.Scripter.Shared;

namespace doob.middler.Action.Scripting.Models
{
    [ScripterModule("EndpointRule")]
    public class EndpointModule: IScripterModule
    {
       
        public ScriptContextResponse Response { get; }
        public ScriptContextRequest Request { get; }
        public SimpleDictionary<object> PropertyBag => _middlerContext.PropertyBag;

        public EndpointOptions Options { get; set; } = new EndpointOptions();

        private readonly IMiddlerContext _middlerContext;
        private readonly IScriptEngine _scriptEngine;
        public EndpointModule(EndpointModuleInitializeParameters endpointModuleInitializeParameters)
        {
            
            _middlerContext = endpointModuleInitializeParameters.MiddlerContext;
            Options.Terminating = endpointModuleInitializeParameters.Terminating;
            _scriptEngine = endpointModuleInitializeParameters.ScriptEngine;
            Request = new ScriptContextRequest(_middlerContext.Request);
            Response = new ScriptContextResponse(_middlerContext.Response, _scriptEngine, Options);
            
        }


        
     
    }

    public class EndpointOptions
    {
        private bool _terminating;

        public bool Terminating
        {
            get => _terminating;
            set => _terminating = value;
        }
    }

    public record EndpointModuleInitializeParameters(IMiddlerContext MiddlerContext, IScriptEngine ScriptEngine, bool Terminating);
}
