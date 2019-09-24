using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpaWebApp.Models.AnimeDAO
{
    public class ParametersSearch
    {
        int page;
        int limit;
        int order;
        int[] kinds;
        int[] statuses;
        int[] ratings;
        int[] ids;
        int[] genres;
        int[] studios;
        ICollection<ParameterExtendInt> score;
        ICollection<ParameterExtendInt> date;
    }
}
