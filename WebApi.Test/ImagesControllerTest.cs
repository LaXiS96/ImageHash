using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Xunit;

namespace LaXiS.ImageHash.WebApi.Test
{
    public class ImagesControllerTest
    {
        private readonly IHost _host;
        private readonly HttpClient _client;

        public ImagesControllerTest()
        {
            try
            {
                File.Delete("Testing.litedb5");
            }
            catch (Exception) { }

            _host = Program.CreateHostBuilder(new string[] { })
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.UseEnvironment("Testing");
                })
                .Start();
            _client = _host.GetTestClient();
        }

        [Fact]
        public async void Post()
        {
            var res = await _client.PostAsync("/images", new StringContent("{\"name\":\"test\"}", Encoding.UTF8, "application/json"));
            Assert.True(res.IsSuccessStatusCode);
        }
    }
}
