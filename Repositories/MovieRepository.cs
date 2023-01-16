using MovieApplication.Model;
using System.Linq;

namespace MovieApplication.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies;
        private readonly List<string> _genres;

        public MovieRepository(List<Movie> movies, List<string> genres)
        {
            _movies = movies;
            _genres = genres;
        }

        public async Task<List<string>> GetAllGenres()
        {
            //I know that it doesn't work async -> usually when we call database or httpClient we don't have this issue
            return _genres;
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            //I know that it doesn't work async -> usually when we call database or httpClient we don't have this issue
            return _movies;
        }

        public async Task<List<Movie>> GetMoviesReleasedAfter(int year)
        {
            //I know that it doesn't work async -> usually when we call database or httpClient we don't have this issue
            var movies = _movies.Where(x => x.ReleaseYear >= year).ToList();
            return movies;
        }

        public async Task<List<Movie>> GetMoviesThatMatchCriteria(string director, string genre, string actor = null)
        {
            //I know that it doesn't work async -> usually when we call database or httpClient we don't have this issue
            var movies = _movies;
            if (!string.IsNullOrEmpty(director))
                movies = movies.Where(x => x.Director.Contains(director, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (movies.Count == 0)
                return new List<Movie>();
            if (!string.IsNullOrEmpty(genre))
            {
                var genreCultureInvariant = _genres.Where(x => x.Equals(genre, StringComparison.InvariantCultureIgnoreCase)).First();
                movies = movies.Where(x => x.Genres.Contains(genreCultureInvariant)).ToList();
            }
            if (movies.Count == 0)
                return new List<Movie>();

            return movies;

        }

        public async Task<bool> SearchForGenre(string genre)
        {
            //I know that it doesn't work async -> usually when we call database or httpClient we don't have this issue
            var isInDb = _genres.Where(x => x.Equals(genre, StringComparison.InvariantCultureIgnoreCase)).Any();
            return isInDb;
        }
    }
}
