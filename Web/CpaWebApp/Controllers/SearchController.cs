using CpaWebApp.Models.Request;
using Microsoft.AspNetCore.Mvc;
using ShikiApiLib;
using System;
using System.Collections.Generic;
using System.Linq;
using CpaWebApp.Providers;
using Microsoft.Extensions.Caching.Memory;
using CpaWebApp.Interfaces;
using CpaWebApp.Models.AnimeDAO;

namespace ShikimoriRandomizer.Controllers
{
    public class SearchController : Controller
    {
        private IAnimeDAO _animeDAO;

        public SearchController(IAnimeDAO animeDAO)
        {
            this._animeDAO = animeDAO;
        }


        [HttpGet]
        [HttpPost]
        [Route("/search/")]
        public ResponseAnime Search([FromQuery] ParametersAnime request)
        {

            // TODO if request == null

            ResponseAnime animes = _animeDAO.Animes(request);

            return animes;
        }

        [HttpGet]
        [HttpPost]
        [Route("/search/")]
        public Anime Random([FromQuery] ParametersAnime request)
        {
            Anime anime = _animeDAO.Random(request);

            return anime;
        }


        // LEGACY START

        [Route("/api/Search/")]
        [HttpGet]
        public IEnumerable<AnimeShortInfo> Get([FromQuery] SearchRequest request)
        {

            Anime anime = _animeDAO.Random(ConvertToParametersAnime(request));

            return new List<AnimeShortInfo>
            {
                new AnimeShortInfo
                {
                    Name = anime.names.First().text,
                    Url = anime.links.First().link
                }
            };
        }

        private ParametersAnime ConvertToParametersAnime(SearchRequest source)
        {
            ParametersAnime parameters = new ParametersAnime()
            {
                phrase = source.Text,
                genres = source.Genres,
                studios = source.Studios
            };

            return parameters;
        }

        // LEGACY END
    }
}
