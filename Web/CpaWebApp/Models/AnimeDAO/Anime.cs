using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CpaWebApp.Models.AnimeDAO
{
    public class Anime
    {
        int id;
        ICollection<MultyName> names;
        MultyImage image;
        ExternalServiceAnime links;
        int kind;
        int status;
        int episodies;
        int episodiesAired;
        DateTime aired;
        DateTime released;
        int score;
        int epidoseDuration;
        ICollection<Studio> studios;
    }
}
