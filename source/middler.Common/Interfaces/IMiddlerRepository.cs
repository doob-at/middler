using System.Collections.Generic;
using doob.middler.Common.SharedModels.Models;

namespace doob.middler.Common.Interfaces
{
    public interface IMiddlerRepository {
        
        List<MiddlerRule> ProvideRules();
    }
   
}
