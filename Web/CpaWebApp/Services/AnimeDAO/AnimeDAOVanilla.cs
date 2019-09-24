using CpaWebApp.Interfaces;
using CpaWebApp.Models.AnimeDAO;
using CpaWebApp.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpaWebApp.Services.AnimeDAO
{
    public class AnimeDAOVanilla : IAnimeDAO
    {
        public Anime Anime(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseAnime Animes(ParametersAnime parameters)
        {
            throw new NotImplementedException();
        }

        public Anime Random()
        {
            ShikimoriProvider shikimoriProvider = new ShikimoriProvider();

            ShikiApiLib.AnimeShortInfo vanillaAnime = shikimoriProvider.GetRandomTitle(new Models.Request.SearchRequest());

            return new Anime();
        }

        public Anime Random(ParametersAnime parameters)
        {
            throw new NotImplementedException();
        }
    }
}
