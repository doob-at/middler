using System;
using System.Collections.Generic;
using System.Linq;

namespace doob.middler.ExtensionMethods
{
    public static class ListExtensions
    {
        public static IEnumerable<string> IgnoreNullOrWhiteSpace(this IEnumerable<string> enumerable)
        {
            return enumerable.Where(e => !String.IsNullOrWhiteSpace(e));
        }
    }
}
