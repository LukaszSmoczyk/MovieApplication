using Microsoft.AspNetCore.Mvc;
using MovieApplication.Model;
using MovieApplication.Repositories;

namespace MovieApplication.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieRepository _movieRepository;
        public MovieController(ILogger<MovieController> logger, IMovieRepository movieRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Returns all movies stored in repositroy
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("movies/all")]
        [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieRepository.GetAllMovies();
            return Ok(movies);
        }

        /// <summary>
        /// Returns all movie genres stored in repositroy
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("genres/all")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _movieRepository.GetAllGenres();
            return Ok(genres);
        }

        /// <summary>
        /// Returns movies that match search criteria
        /// </summary>
        /// <param name="year">Release year</param>
        /// <returns>Returns movies that where released in or after specified year</returns>
        [HttpGet]
        [Route("movies/{year}")]
        [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMoviesReleasedAfterProvidedYear(
            [FromRoute] int year)
        {
            //Validation of request
            if (year <= 0)
                return BadRequest("Value of provided year has to be greater than 0");

            var movies = await _movieRepository.GetMoviesReleasedAfter(year);
            return Ok(movies);
        }

        /// <summary>
        /// Search for movies that match criteria
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="director"></param>
        /// <returns>List of movies</returns>
        [HttpGet]
        [Route("movies/search")]
        [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMoviesMatchingCriteria(
                [FromQuery]string genre,
                [FromQuery]string director)
        {

            //Search for genre in db
            var isGenreInDb = await _movieRepository.SearchForGenre(genre);
            if (isGenreInDb == false)
                return BadRequest($"Didn't find movie genre: {genre} in our base. Please try again");

            var movies = await _movieRepository.GetMoviesThatMatchCriteria(director, genre);
            if (movies.Count > 0)
                _logger.LogInformation($"Found {movies.Count} results");
            else
                _logger.LogInformation($"Didn't found any results");
            return Ok(movies);
        }
    }
}
