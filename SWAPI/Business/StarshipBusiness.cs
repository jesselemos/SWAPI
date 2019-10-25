using SWAPI.Helpers;
using SWAPI.Models;
using System.Collections.Generic;

namespace SWAPI.Business
{
    public static class StarshipBusiness
    {
        public static List<StarshipModel> GetStarshipStops(ulong distance)
        {
            List<StarshipModel> starships = APICall.GetStarshipResults()?.results;
            CalculateStarshipStops(ref starships, distance);
            return starships;
        }

        public static void CalculateStarshipStops(ref List<StarshipModel> starships, ulong distance)
        {
            if (starships == null || distance == 0)
            {
                return;
            }

            foreach (StarshipModel starship in starships)
            {
                ulong.TryParse(starship.MGLT, out ulong mglt);

                ulong consume = GetSpentPerHour(starship.consumables);
                if (mglt == 0 || consume == 0)
                {
                    continue;
                }

                starship.stops = distance / (mglt * consume);
            }
        }

        public static ulong GetSpentPerHour(string consumables)
        {
            string[] splited = consumables?.Split(' ');
            if (splited == null || splited.Length < 2)
            {
                return 0;
            }

            ulong hours = 0;
            ulong.TryParse(splited[0], out ulong unit);

            switch (splited[1].ToLower())
            {
                case "hour":
                case "hours":
                    hours = 1;
                    break;
                case "day":
                case "days":
                    hours = 24;
                    break;
                case "week":
                case "weeks":
                    hours = 7 * 24;
                    break;
                case "month":
                case "months":
                    hours = 30 * 24;
                    break;
                case "year":
                case "years":
                    hours = 365 * 24;
                    break;
            }

            return unit * hours;
        }
    }
}
