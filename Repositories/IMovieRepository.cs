using MovieApplication.Model;

namespace MovieApplication.Repositories
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>List of movies</returns>
        public Task<List<Movie>> GetAllMovies();

        /// <summary>
        /// Get all genres
        /// </summary>
        /// <returns>List of genres</returns>
        public Task<List<string>> GetAllGenres();

        /// <summary>
        /// Get all movies released after specified year 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of movies released after specified date (movies realeased in the same year will also be returned)</returns>
        public Task<List<Movie>> GetMoviesReleasedAfter(int year);

        /// <summary>
        /// Search for genre 
        /// </summary>
        /// <param name="genre">e.g. comedy</param>
        /// <returns>Bool</returns>
        public Task<bool> SearchForGenre(string genre);

        /// <summary>
        /// Search for movies that match query
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of movies</returns>
        public Task<List<Movie>> GetMoviesThatMatchCriteria(string director, string genre, string actor = null);
    }
}
