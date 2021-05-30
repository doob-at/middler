using System.Collections.Generic;
using System.Linq;
using doob.middler.Common.Interfaces;
using doob.middler.Common.SharedModels.Models;

namespace doob.middler
{
    public class InMemoryRepo: IMiddlerRepository
    {

        private List<MiddlerRule> Endpoints { get; }


        public InMemoryRepo(): this(null)
        {

        }

        public InMemoryRepo(IEnumerable<MiddlerRule>? middlerRules)
        {

            Endpoints = middlerRules?.ToList() ?? new List<MiddlerRule>();
        }

        public List<MiddlerRule> ProvideRules()
        {
            return Endpoints;
        }


        internal void AddRule(params MiddlerRule[] middlerRules)
        {
            Endpoints.AddRange(middlerRules);
        }
    }
}
