using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace LaXiS.ImageHash.WebApi.Test
{
    public class WebApiFixture : IDisposable
    {
        public HttpClient Client;

        private readonly IHost _host;
        private readonly string _databaseName;
        private string _connectionString;

        public WebApiFixture()
        {
            _databaseName = $"ImageHash-{ObjectId.GenerateNewId()}";

            _host = Program.CreateHostBuilder(new string[] { })
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.UseEnvironment("Testing");
                })
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["MongoDb:Database"] = _databaseName
                    });
                })
                .ConfigureServices((context, services) =>
                {
                    _connectionString = context.Configuration.GetValue<string>("MongoDb:ConnectionString");
                })
                .Start();
            Client = _host.GetTestClient();
        }

        public void Dispose()
        {
            MongoClient client = new MongoClient(_connectionString);
            client.DropDatabase(_databaseName);
        }
    }
}
