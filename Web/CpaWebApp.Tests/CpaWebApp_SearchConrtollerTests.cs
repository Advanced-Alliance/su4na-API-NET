using CpaWebApp.Services.AnimeDAO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ShikimoriRandomizer.Controllers;
using System;
using System.Collections.Generic;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;
using Xunit;

namespace CpaWebApp.Tests
{
    public class CpaWebApp_SearchConrtollerTests
    {

        private FluentMockServer _server;
        private IConfigurationBuilder _configBuilder;

        public CpaWebApp_SearchConrtollerTests()
        {
            _configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            _server = FluentMockServer.Start(port: 443, ssl: true);
        }

        [Fact]
        public void GetRandomAnime()
        {



            // Arrange

            _server
            .Given(Request.Create().WithPath("/api/animes").WithParam(p => p.ContainsKey("page")).UsingGet())
            .RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-type", "application/json")
                .WithBody(@"")
            );


            _server
            .Given(Request.Create().WithPath("/api/animes").WithParam(p => !p.ContainsKey("page") || Int32.Parse(p["page"][0])>1).UsingGet())
            // Int32.TryParse(p["page"][0]) && Int32.Parse(p["page"][0])>1
            .RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-type", "application/json")
                .WithBody(@"[
                              {
                                ""id"": 5114,
                                ""name"": ""Fullmetal Alchemist: Brotherhood"",
                                ""russian"": ""Стальной алхимик: Братство"",
                                ""image"": {
                                            ""original"": ""/system/animes/original/5114.jpg?1561450766"",
                                  ""preview"": ""/system/animes/preview/5114.jpg?1561450766"",
                                  ""x96"": ""/system/animes/x96/5114.jpg?1561450766"",
                                  ""x48"": ""/system/animes/x48/5114.jpg?1561450766""
                                },
                                ""url"": ""/animes/5114-fullmetal-alchemist-brotherhood"",
                                ""kind"": ""tv"",
                                ""status"": ""released"",
                                ""episodes"": 64,
                                ""episodes_aired"": 0,
                                ""aired_on"": ""2009-04-05"",
                                ""released_on"": ""2010-07-04""
                              }
                            ]")
            );
            var memCache = new MemoryCache(new MemoryCacheOptions());


            SearchController controller = new SearchController(new AnimeDAOVanilla(memCache, _configBuilder.Build()));

            //Act

            // Получение результата из контроллера и имени первого (единственного) ответа в списке.
            // Временная версия теста, но пока так
            var resultNameEnum = controller.Get(new Models.Request.SearchRequest
            {

            }).GetEnumerator();
            resultNameEnum.MoveNext();
            var resultName = resultNameEnum.Current.Name;

            //Assert
            Assert.Equal("Fullmetal Alchemist: Brotherhood", resultName);
        }

        [Fact]
        public void GetAnimes()
        {



            // Arrange

            _server
            .Given(Request.Create().WithPath("/api/animes").WithParam(p => p.ContainsKey("page")).UsingGet())
            .RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-type", "application/json")
                .WithBody(@"")
            );


            _server
            .Given(Request.Create().WithPath("/api/animes").WithParam(p => !p.ContainsKey("page") || Int32.Parse(p["page"][0]) > 1).UsingGet())
            // Int32.TryParse(p["page"][0]) && Int32.Parse(p["page"][0])>1
            .RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-type", "application/json")
                .WithBody(@"[
                              {
                                ""id"": 5114,
                                ""name"": ""Fullmetal Alchemist: Brotherhood"",
                                ""russian"": ""Стальной алхимик: Братство"",
                                ""image"": {
                                            ""original"": ""/system/animes/original/5114.jpg?1561450766"",
                                  ""preview"": ""/system/animes/preview/5114.jpg?1561450766"",
                                  ""x96"": ""/system/animes/x96/5114.jpg?1561450766"",
                                  ""x48"": ""/system/animes/x48/5114.jpg?1561450766""
                                },
                                ""url"": ""/animes/5114-fullmetal-alchemist-brotherhood"",
                                ""kind"": ""tv"",
                                ""status"": ""released"",
                                ""episodes"": 64,
                                ""episodes_aired"": 0,
                                ""aired_on"": ""2009-04-05"",
                                ""released_on"": ""2010-07-04""
                              }
                            ]")
            );
            var memCache = new MemoryCache(new MemoryCacheOptions());


            SearchController controller = new SearchController(new AnimeDAOVanilla(memCache, _configBuilder.Build()));

            //Act

            var resultAnimes = controller.Search(null);
            var animeMock = resultAnimes.animes[0];
            var animeStub = resultAnimes.animes[1];

            //Assert
            // Проверка на получение данных из мока
            Assert.Equal("Fullmetal Alchemist: Brotherhood", animeMock.names[0].text); //Вообще я точно не помню какой язык добавляется первее, но для нормальной версии 100% этот код не подойдёт никогда

            // Проверка на получение сообщения о временной неподдержке метода
            // Это очень временный тест, с нормальными версиями IAnimeDAO он не имеет ничего общего,
            // но для реализации AnimeDAOVanilla другого тестирования реализовать невозможно.
            Assert.Equal("Вывод списка пока не поддерживается", animeStub.names[0].text);
        }
    }
}
