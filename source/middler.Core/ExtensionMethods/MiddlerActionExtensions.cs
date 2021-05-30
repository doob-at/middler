using doob.middler.Common.SharedModels.Models;

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

    }
}
