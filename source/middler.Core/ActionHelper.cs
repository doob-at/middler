using doob.middler.Common;
using doob.middler.Common.Interfaces;
using doob.Reflectensions.Common;


namespace doob.middler
{
    public class ActionHelper: IActionHelper
    {
        private IMiddlerRequestContext _middlerRequestContext;

        public ActionHelper(IMiddlerRequestContext middlerRequestContext)
        {
            _middlerRequestContext = middlerRequestContext;
        }

        //public string BuildPathFromRoutData(string template)
        //{


        //    var queryObj = new ScriptObject
        //    {
        //        ["*"] = _middlerRequestContext.Uri.Query.ToNull()?.Substring(1)
        //    };

        //    queryObj.Import(_middlerRequestContext.QueryParameters, renamer:member => member.Name);

        //    foreach (var (key, value) in _middlerRequestContext.QueryParameters)
        //    {
        //        queryObj[key] = value;
        //    }

        //    var uriObj = new ScriptObject();
        //    uriObj.Import(_middlerRequestContext.Uri, renamer: member => member.Name);

        //    var routeObj = new ScriptObject();
        //        routeObj.Import(_middlerRequestContext.RouteData);

        //    var scriptObj = new ScriptObject
        //    {
        //        ["Route"] = routeObj,
        //        ["Query"] = queryObj,
        //        ["Uri"] = uriObj
        //    };


        //    var scribanTemplate = Template.Parse(template);
        //    var result = scribanTemplate.Render(scriptObj, member => member.Name);

        //    return result;

        //    //string ProcessHtmlTag(Match m)
        //    //{
        //    //    string part = m.Groups["part"].Value;

        //    //    if (part.StartsWith("@"))
        //    //    {
        //    //        part = part.Substring(1);
        //    //        try
        //    //        {
        //    //            return _middlerRequestContext.Uri.GetPropertyValue<object>(part).ToString();
        //    //        }
        //    //        catch
        //    //        {
        //    //            // ignored
        //    //        }
        //    //    }

        //    //    if (part.StartsWith("?"))
        //    //    {
        //    //        part = part.Substring(1);
        //    //        if (part == "*")
        //    //        {
        //    //            return _middlerRequestContext.Uri.Query?.Substring(1);
        //    //        }
        //    //        return _middlerRequestContext.QueryParameters.TryGetValue(part, out var qp) ? qp : null;
        //    //    }

        //    //    return _middlerRequestContext.RouteData.TryGetValue(part, out var rd) ? rd.ToString() : null;

        //    //}

        //    //Regex regex = new Regex("{(?<part>([?@a-zA-Z0-9*]*))}");
        //    //string cleanString = regex.Replace(template, ProcessHtmlTag);
        //    //return cleanString;
        //}
    }
}
