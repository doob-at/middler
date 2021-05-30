using System;
using doob.middler.Common.SharedModels.Models;

namespace doob.middler.Common
{
    public interface IMiddlerContext
    {
        IMiddlerRequestContext Request { get; }

        IMiddlerResponseContext Response { get; }

        SimpleDictionary<object> PropertyBag { get; }

        IServiceProvider RequestServices { get; }

    }
}
