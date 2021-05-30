using System;
using System.Collections.Generic;
using doob.middler.Common.Interfaces;
using doob.middler.Common.SharedModels.Models;
using Microsoft.Extensions.DependencyInjection;

namespace doob.middler.Map {
    public class MiddlerMapActionsBuilder : IMiddlerMapActionsBuilder {
        public IServiceProvider ServiceProvider { get; }
        public IMiddlerOptions MiddlerOptions { get; }
        public List<MiddlerAction> MiddlerActions { get; } = new List<MiddlerAction>();


        public MiddlerMapActionsBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            MiddlerOptions = serviceProvider.GetRequiredService<IMiddlerOptions>();
        }

        public IMiddlerMapActionsBuilder AddAction(MiddlerAction action) {
            MiddlerActions.Add(action);
            return this;
        }

        public IMiddlerMapActionsBuilder AddAction<T>() where T : MiddlerAction {

            var act = ActivatorUtilities.CreateInstance<T>(ServiceProvider);
            MiddlerActions.Add(act);
            return this;
        }

        public IMiddlerMapActionsBuilder AddAction<T, TParam>(TParam parameters) where T : MiddlerAction<TParam> where TParam : class, new()
        {

            var intHelper = new InternalHelper(ServiceProvider);

            var actionType = intHelper.GetRegisteredActionTypeAlias<T>();
            if (actionType == null)
                return this;


            var act = ActivatorUtilities.CreateInstance<T>(ServiceProvider);
            act.Parameters = parameters;
            var basAct = intHelper.ConvertToBasicMiddlerAction(act);

            
            MiddlerActions.Add(basAct);
            return this;
        }

        public IMiddlerMapActionsBuilder AddAction<T, TParam>(Action<TParam> parameters) where T : MiddlerAction<TParam> where TParam : class, new() {

            var param = new TParam();
            parameters.Invoke(param);
            return AddAction<T, TParam>(param);
        }
    }
}
