using LaXiS.ImageHash.Models.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using Xunit;

namespace LaXiS.ImageHash.WebApi.Test
{
    public class TagsControllerTest : IClassFixture<WebApiFixture>
    {
        private static readonly string ControllerUri = "/tags";

        private readonly WebApiFixture _fixture;

        public TagsControllerTest(
            WebApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void Post()
        {
            var obj = new TagWriteResource
            {
                Name = "TestPost"
            };

            var res = await _fixture.Client.PostAsync(ControllerUri, HttpUtils.ObjectToJson(obj));
            Assert.True(res.IsSuccessStatusCode);

            res = await _fixture.Client.PostAsync(ControllerUri, HttpUtils.ObjectToJson(obj));
            Assert.False(res.IsSuccessStatusCode);
        }

        [Fact]
        public async void Get()
        {
            var obj = new TagWriteResource
            {
                Name = "TestGet"
            };

            var res = await _fixture.Client.PostAsync(ControllerUri, HttpUtils.ObjectToJson(obj));
            Assert.True(res.IsSuccessStatusCode);

            res = await _fixture.Client.GetAsync(ControllerUri);
            Assert.True(res.IsSuccessStatusCode);

            Assert.Contains(res.AsJson(), t =>
                (t as JObject).GetValue(nameof(TagWriteResource.Name), StringComparison.OrdinalIgnoreCase).ToString() == obj.Name);
        }

        [Fact]
        public async void Put()
        {
            var obj = new TagWriteResource
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

            Assert.True((res.AsJson() as JObject).GetValue(nameof(TagWriteResource.Name), StringComparison.OrdinalIgnoreCase).ToString() == obj.Name);
        }

        [Fact]
        public async void Delete()
        {
            var obj = new TagWriteResource
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
