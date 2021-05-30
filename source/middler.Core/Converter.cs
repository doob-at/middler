using System;
using doob.middler.JsonConverters;
using doob.Reflectensions;

namespace doob.middler
{
    public class Converter
    {
        private static readonly Lazy<Json> lazyJson = new Lazy<Json>(() => new Json()
            .RegisterJsonConverter<DecimalJsonConverter>()
        );

        public static Json Json => lazyJson.Value;

    }
}
