using RestSharp;
using SWAPI.Models;

namespace SWAPI.Helpers
{
    public static class APICall
    {
        private static readonly RestClient _client = new RestClient();

        public static StarshipResultModel GetStarshipResults()
        {
            StarshipResultModel starships = _client.Execute<StarshipResultModel>(new RestRequest("https://swapi.co/api/starships", Method.GET))?.Data;
            if (starships?.results == null)
            {
                return starships;
            }

            while (!string.IsNullOrWhiteSpace(starships?.next))
            {
                StarshipResultModel newStarships = _client.Execute<StarshipResultModel>(new RestRequest(starships.next, Method.GET))?.Data;
                if (newStarships?.results == null)
                {
                    continue;
                }

                starships.results.AddRange(newStarships.results);
                starships.next = newStarships.next;
            }

            return starships;
        }
    }
}
