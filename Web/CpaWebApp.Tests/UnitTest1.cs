using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;
using Xunit;

namespace CpaWebApp.Tests
{
    public class UnitTest1
    {

        private FluentMockServer _server;

        public UnitTest1()
        {

            _server = FluentMockServer.Start(new FluentMockServerSettings
            {
                Urls = new[] { "http://shikimori.one:80", "https://shikimori.one:443" }
            });

            //_server = FluentMockServer.Start("http://shikimori.one:80");
        }

        [Fact]
        public void Test1()
        {
            _server
            .Given(Request.Create().WithPath("/").UsingGet())
            .RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-type", "application/json")
                .WithBody(@"{ ""msg"": ""Hello world!"" }")
            );


        }
    }
}
