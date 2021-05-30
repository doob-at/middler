using System;
using System.Collections.Generic;
using System.Linq;
using doob.middler.Common.Interfaces;
using doob.middler.Common.SharedModels.Enums;
using doob.middler.Common.SharedModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace doob.middler.Models {
    public class MiddlerOptions : IMiddlerOptions {
        public AccessMode DefaultAccessMode { get; set; } = AccessMode.Allow;

        public List<string> DefaultHttpMethods { get; set; } = new List<string>();

        public List<string> DefaultScheme { get; set; } = new List<string>();

        public int AutoStreamDefaultMemoryThreshold { get; set; } = 131072;

        public Dictionary<string, Type> RegisteredActionTypes { get; } = new Dictionary<string, Type>();
    }


    public class MiddlerOptionsBuilder: IMiddlerOptionsBuilder
    {
        public IMiddlerOptions Options { get; } = new MiddlerOptions();
        public  IServiceCollection ServiceCollection { get; }

        public MiddlerOptionsBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public IMiddlerOptionsBuilder SetDefaultAccessMode(AccessMode accessMode)
        {
            Options.DefaultAccessMode = accessMode;
            return this;
        }


        public IMiddlerOptionsBuilder SetDefaultHttpMethods(IEnumerable<string> httpMethods)
        {
            Options.DefaultHttpMethods = httpMethods.ToList();
            return this;
        }

        public IMiddlerOptionsBuilder SetDefaultHttpMethods(params string[] httpMethods)
        {
            Options.DefaultHttpMethods = httpMethods.ToList();
            return this;
        }


        public IMiddlerOptionsBuilder SetDefaultScheme(IEnumerable<string> schemes)
        {
            Options.DefaultScheme = schemes.ToList();
            return this;
        }

        public IMiddlerOptionsBuilder SetDefaultScheme(params string[] schemes)
        {
            Options.DefaultScheme = schemes.ToList();
            return this;
        }

        public IMiddlerOptionsBuilder SetAutoStreamDefaultMemoryThreshold(int value)
        {
            Options.AutoStreamDefaultMemoryThreshold = value;
            return this;
        }



        public IMiddlerOptionsBuilder RegisterAction<T>(string alias) where T: IMiddlerAction
        {
            return RegisterAction(alias, typeof(T));
        }

        public IMiddlerOptionsBuilder RegisterAction(string alias, Type actionType)
        {
            Options.RegisteredActionTypes.TryAdd(alias, actionType);
            return this;
        }

        
    }

   
}
