using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace LaXiS.ImageHash.WebApi.Test
{
    public static class HttpExtensions
    {
        public static JToken AsJson(this HttpResponseMessage message)
        {
            return JToken.Parse(
                message.Content.ReadAsStringAsync().Result);
        }
    }
}
