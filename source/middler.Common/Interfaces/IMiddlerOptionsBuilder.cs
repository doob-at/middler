using System;
using System.Collections.Generic;
using doob.middler.Common.SharedModels.Enums;
using doob.middler.Common.SharedModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace doob.middler.Common.Interfaces
{
    
    public interface IMiddlerOptionsBuilder
    {
        IMiddlerOptions Options { get; }
        IServiceCollection ServiceCollection { get; }
        IMiddlerOptionsBuilder SetDefaultAccessMode(AccessMode accessMode);
        IMiddlerOptionsBuilder SetDefaultHttpMethods(IEnumerable<string> httpMethods);
        IMiddlerOptionsBuilder SetDefaultHttpMethods(params string[] httpMethods);
        IMiddlerOptionsBuilder SetDefaultScheme(IEnumerable<string> schemes);
        IMiddlerOptionsBuilder SetDefaultScheme(params string[] schemes);
        IMiddlerOptionsBuilder SetAutoStreamDefaultMemoryThreshold(int value);

        IMiddlerOptionsBuilder RegisterAction<T>(string alias) where T : IMiddlerAction;
        IMiddlerOptionsBuilder RegisterAction(string alias, Type actionType);

    }
}