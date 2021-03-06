using System.IO;
using System.Text;
using doob.middler.Common;
using doob.middler.Common.SharedModels.Models;
using doob.Reflectensions;

namespace doob.middler.Context
{
    public class MiddlerResponseContext : IMiddlerResponseContext
    {
        //private FakeHttpContext FakeHttpContext { get; set; }

        public int StatusCode { get; set; }
        public SimpleDictionary<string> Headers { get; set; } = new SimpleDictionary<string>();

        public Stream Body { get; internal set; } = Stream.Null;


        public MiddlerResponseContext()
        {

        }

        public string GetBodyAsString()
        {

            using var sr = new StreamReader(Body);
            Body.Seek(0, SeekOrigin.Begin);
            return sr.ReadToEnd();

        }

        public void SetBody(object? body)
        {

            Body.Seek(0, SeekOrigin.Begin);
            
            Body.SetLength(0);

            if (body == null)
            {
                return;
            }

            switch (body)
            {
                case string str:
                    {
                        SetStringBody(str);
                        return;
                    }
                case Stream stream:
                    {
                        SetStreamBody(stream);
                        return;
                    }
                default:
                    {
                        SetObjectBody(body);
                        break;
                    }
            }


        }

        private void SetObjectBody(object @object)
        {
            using var sw = new StreamWriter(Body, new UTF8Encoding(false), 8192, true);
            Json.Converter.JsonSerializer.Serialize(sw, @object);
        }

        private void SetStringBody(string content)
        {
            using var sw = new StreamWriter(Body, new UTF8Encoding(false), 8192, true);
            sw.Write(content);
        }

        private void SetStreamBody(Stream stream)
        {
            Body = stream;
        }
    }
}
