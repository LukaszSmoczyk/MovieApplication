using MovieApplication.Model;
using Newtonsoft.Json;

namespace MovieApplication.Data
{
    public class SeedData
    {
        public static List<Movie> SeedMovieData(string filePath)
        {
            var movies = new List<Movie>();

            var seedData = Directory
                .GetFiles(filePath, "*.json", SearchOption.AllDirectories)
                .Where(x => x.Contains("MovieSeedData", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();

            if (seedData == null)
                throw new Exception($"Couldn't find seed data in {filePath}");

            using (StreamReader reader = new StreamReader(seedData))
            {
                string json = reader.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
            return movies;
        }

        public static List<string> SeedGenreData(string filePath)
        {
            var genres = new List<string> { 
                                                    "Comedy",
                                                    "Fantasy",
                                                    "Crime",
                                                    "Drama",
                                                    "Music",
                                                    "Adventure",
                                                    "History",
                                                    "Thriller",
                                                    "Animation",
                                                    "Family",
                                                    "Mystery",
                                                    "Biography",
                                                    "Action",
                                                    "Film-Noir",
                                                    "Romance",
                                                    "Sci-Fi",
                                                    "War",
                                                    "Western",
                                                    "Horror",
                                                    "Musical",
                                                    "Sport" };
            return genres;
        }
    }
}
