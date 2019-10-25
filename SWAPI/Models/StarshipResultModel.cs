using System.Collections.Generic;

namespace SWAPI.Models
{
    public class StarshipResultModel
    {
        public long count { get; set; }
        public string next { get; set; }
        public List<StarshipModel> results { get; set; }
    }
}
