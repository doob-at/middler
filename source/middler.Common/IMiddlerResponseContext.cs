using doob.middler.Common.SharedModels.Models;

namespace doob.middler.Common
{
    public interface IMiddlerResponseContext
    {
        int StatusCode { get; set; }
        SimpleDictionary<string> Headers { get; set; }

        string GetBodyAsString();
        void SetBody(object? body);
    }
}
