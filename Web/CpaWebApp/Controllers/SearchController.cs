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
    [Route("/api/Search/")]
    public class SearchController : Controller
    {
        private IMemoryCache _cache;
        private IAnimeDAO _animeDAO;

        public SearchController(IMemoryCache cache, IAnimeDAO animeDAO)
        {
            this._cache = cache;
            this._animeDAO = animeDAO;
        }

        [HttpGet]
        public IEnumerable<Anime> Get([FromQuery] ParametersAnime request)
        {

            Anime anime = _animeDAO.Random(request);

            //var provider = new ShikimoriProvider(_cache);
            return new List<Anime> { anime };
        }
    }
}
