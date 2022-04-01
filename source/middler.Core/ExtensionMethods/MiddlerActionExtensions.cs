using doob.middler.Common.SharedModels.Interfaces;
using doob.middler.Common.SharedModels.Models;
using doob.Reflectensions.ExtensionMethods;

namespace doob.middler.ExtensionMethods {
    public static class MiddlerActionExtensions {

        public static MiddlerAction ToBasicMiddlerAction<T>(this MiddlerAction<T> middlerAction) where T : class, new()
        {
            var act = new MiddlerAction
            {
                Terminating = middlerAction.Terminating,
                WriteStreamDirect = middlerAction.WriteStreamDirect,
                ActionType = middlerAction.ActionType,
                Parameters = Converter.Json.ToJObject(middlerAction.Parameters)
                //middlerAction.Parameters?.GetType().GetProperties()
                //    .ToDictionary(p => p.Name, p => p.GetValue(middlerAction.Parameters)) ??
                //new Dictionary<string, object>()
            };

            return act;
        }

        //public static MiddlerAction ToBasicMiddlerAction(this IMiddlerAction middlerAction)
        //{
        //    var act = new MiddlerAction
        //    {
        //        Terminating = middlerAction.Terminating,
        //        WriteStreamDirect = middlerAction.WriteStreamDirect,
        //        ActionType = middlerAction.ActionType
        //    };

        //    var parametersProperty = middlerAction.GetType().GetProperty("Parameters");
        //    if (parametersProperty != null)
        //    {
        //        var parameters = parametersProperty.GetValue(middlerAction)!;
        //        act.Parameters = Converter.Json.ToJObject(parameters);
        //    }

        //    return act;
        //}

    }
}
