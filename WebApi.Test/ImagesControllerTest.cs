using LaXiS.ImageHash.Models.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using Xunit;

namespace LaXiS.ImageHash.WebApi.Test
{
    public class ImagesControllerTest : IClassFixture<WebApiFixture>
    {
        private static readonly string ControllerUri = "/images";

        private readonly WebApiFixture _fixture;

        public ImagesControllerTest(
            WebApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void Post()
        {
            var obj = new ImageWriteResource
            {
                Name = "TestPost",
                Tags = new string[] { "TestPost" }
            };

            var res = await _fixture.Client.PostAsync(ControllerUri, HttpUtils.ObjectToJson(obj));
            Assert.True(res.IsSuccessStatusCode);

            res = await _fixture.Client.PostAsync(ControllerUri, HttpUtils.ObjectToJson(obj));
            Assert.False(res.IsSuccessStatusCode);
        }

        [Fact]
        public async void Get()
        {
            var obj = new ImageWriteResource
            {
                Name = "TestGet",
                Tags = new string[] { "TestGet1", "TestGet2" }
            };

            var res = await _fixture.Client.PostAsync(ControllerUri, HttpUtils.ObjectToJson(obj));
            Assert.True(res.IsSuccessStatusCode);

            res = await _fixture.Client.GetAsync($"{ControllerUri}?$filter=name eq 'TestGet'");
            Assert.True(res.IsSuccessStatusCode);

            Assert.Contains(res.AsJson(), t =>
                (t as JObject).GetValue(nameof(ImageWriteResource.Name), StringComparison.OrdinalIgnoreCase).ToString() == obj.Name);
        }

        [Fact]
        public async void Put()
        {
            var obj = new ImageWriteResource
            {
                Name = "TestPut"
            };

            var res = await _fixture.Client.PostAsync(ControllerUri, HttpUtils.ObjectToJson(obj));
            Assert.True(res.IsSuccessStatusCode);
            var id = res.AsJson()["id"];

            obj.Name = "TestPutMod";

            res = await _fixture.Client.PutAsync($"{ControllerUri}/{id}", HttpUtils.ObjectToJson(obj));
            Assert.True(res.IsSuccessStatusCode);

            res = await _fixture.Client.GetAsync($"{ControllerUri}/{id}");
            Assert.True(res.IsSuccessStatusCode);

            Assert.True((res.AsJson() as JObject).GetValue(nameof(ImageWriteResource.Name), StringComparison.OrdinalIgnoreCase).ToString() == obj.Name);
        }

        [Fact]
        public async void Delete()
        {
            var obj = new ImageWriteResource
            {
                Name = "TestDelete"
            };

            var res = await _fixture.Client.PostAsync(ControllerUri, HttpUtils.ObjectToJson(obj));
            Assert.True(res.IsSuccessStatusCode);
            var id = res.AsJson()["id"];

            res = await _fixture.Client.DeleteAsync($"{ControllerUri}/{id}");
            Assert.True(res.IsSuccessStatusCode);

            res = await _fixture.Client.GetAsync($"{ControllerUri}/{id}");
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }
    }
}
