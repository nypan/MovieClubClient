using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubClient
{
    public class Movie
    {
    //{
    //"id": "tt0232632",
    //"title": "Shiner",
    //"year": 2000,
    //"runtimeMinutes": 99,
    //"genres": [
    //  "Crime",
    //  "Drama",
    //  "Thriller"
    //]
    //}
    [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("title", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Title { get; set; }

        [Newtonsoft.Json.JsonProperty("year", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Year { get; set; }

        [Newtonsoft.Json.JsonProperty("runtimeMinutes", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int RuntimeMinutes { get; set; }

        [Newtonsoft.Json.JsonProperty("genres", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> Genres { get; set; }
    }
}
