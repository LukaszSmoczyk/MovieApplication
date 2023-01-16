using Newtonsoft.Json;

namespace MovieApplication.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [JsonProperty("Year")]
        public int ReleaseYear { get; set; }
        public int Runtime { get; set; }
        public List<string> Genres { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public Uri PosterUri { get; set; }
    }
}
