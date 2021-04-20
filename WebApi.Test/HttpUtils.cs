using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace LaXiS.ImageHash.WebApi.Test
{
    public static class HttpUtils
    {
        public static HttpContent ObjectToJson(object obj)
        {
            return new StringContent(
                JsonConvert.SerializeObject(obj),
                Encoding.UTF8,
                "application/json");
        }
    }
}
