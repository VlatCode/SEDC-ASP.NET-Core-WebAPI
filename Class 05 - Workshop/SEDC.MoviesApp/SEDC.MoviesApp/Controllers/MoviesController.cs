using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.MoviesApp.DTOs;
using SEDC.MoviesApp.Enums;
using SEDC.MoviesApp.Models;

namespace SEDC.MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // Get movie by id using parameter
        [HttpGet("{id}")]
        public ActionResult<MovieDto> GetMovie (int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("The id can not be negative!");
                }

                if (id > StaticDb.Movies.Count)
                {
                    return BadRequest("The id must be within the length of the database!");
                }

                Movie movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound($"Movie with id {id} does not exist!");
                }

                var movieDto = new MovieDto
                {
                    Id = id,
                    Title = movieDb.Title,
                    Description = movieDb.Description,
                    Year = movieDb.Year,
                    Genre = movieDb.Genre,
                };

                return Ok(movieDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        // Get movie by id using a query string
        [HttpGet("queryString")]
        public ActionResult <MovieDto> GetMovieById(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is required!");
                }

                if (id <= 0)
                {
                    return BadRequest("Bad request, the id can not be negative");
                }

                Movie movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id.Value);
                if (movieDb == null)
                {
                    return NotFound("There is no such movie");
                }

                return Ok(new MovieDto
                {
                    Id = id.Value,
                    Title = movieDb.Title,
                    Description = movieDb.Description,
                    Year = movieDb.Year,
                    Genre = movieDb.Genre,
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        // Get all movies
        [HttpGet]
        public ActionResult<List<MovieDto>> GetAllMovies()
        {
            try
            {
                var moviesDb = StaticDb.Movies;
                var movies = moviesDb.Select(x => new MovieDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Year = x.Year,
                    Genre = x.Genre,
                }).ToList();

                return Ok(movies);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        // Filter movies by genre and year
        [HttpGet("filterMovies")]
        public ActionResult<List<Movie>> FilterMovies(string? genre, int? year)
        {
            try
            {
                if (genre == null && year == null)
                {
                    return BadRequest("Bad request. At least one filter must be used!");
                }

                if (genre != null) {
                    var enumValues = Enum.GetValues(typeof(Genre))
                                            .Cast<Genre>()
                                            .Select(v => v.ToString().ToLower())
                                            .ToList();

                    if (enumValues.Contains(genre.ToLower()) == false)
                    {
                        return BadRequest("Please enter a valid genre!");
                    }
                }

                if (year == null)
                {
                    List<Movie> filteredMovies = StaticDb.Movies.Where(x => x.Genre.ToString().ToLower() == genre.ToLower()).ToList();
                    return Ok(filteredMovies);
                }

                if (genre == null)
                {
                    List<Movie> filteredMovies = StaticDb.Movies.Where(x => x.Year == year).ToList();
                    return Ok(filteredMovies);
                }

                List<Movie> filteredMoviesDb = StaticDb.Movies.Where(x => x.Genre.ToString().ToLower() == genre.ToLower()).Where(x => x.Year == year).ToList();

                return Ok(filteredMoviesDb);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        // Add a new movie
        [HttpPost("addMovie")]
        public IActionResult AddMovie([FromBody] AddMovieDto addMovieDto)
        {
            try
            {
                if (string.IsNullOrEmpty(addMovieDto.Title))
                {
                    return BadRequest("Title is a required field!");
                }

                if (string.IsNullOrEmpty(addMovieDto.Description))
                {
                    return BadRequest("Description is a required field!");
                }

                if (addMovieDto.Year == null || addMovieDto.Year <= 0)
                {
                    return BadRequest("Year must not be empty or negative!");
                }

                if (addMovieDto.Genre == null)
                {
                    return BadRequest("Genre is a required field!");
                }

                // map to DTO
                Movie newMovie = new Movie
                {
                    Id = ++StaticDb.MovieId, // incrementing the newly added static StaticDb.NoteId
                    Title = addMovieDto.Title,
                    Description = addMovieDto.Description,
                    Year = addMovieDto.Year,
                    Genre = addMovieDto.Genre
                };

                // add to StaticDb.Movies
                StaticDb.Movies.Add(newMovie);
                return StatusCode(StatusCodes.Status201Created, "Movie added!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        // Update a movie
        [HttpPut]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto updateMovieDto)
        {
            try
            {
                //validations
                Movie movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == updateMovieDto.Id);
                if (movieDb == null)
                {
                    return NotFound("Movie not found!");
                }

                if (string.IsNullOrEmpty(updateMovieDto.Title))
                { 
                    return BadRequest("Title must not be empty");
                }

                if (string.IsNullOrEmpty(updateMovieDto.Description))
                {
                    return BadRequest("Description must not be empty!");
                }

                if (updateMovieDto.Year == null)
                {
                    return BadRequest("Year must not be empty");
                }

                if (updateMovieDto.Year <= 0)
                {
                    return BadRequest("Year must not be negative!");
                }

                if (updateMovieDto.Genre == null)
                {
                    return BadRequest("Genre must not be empty");
                }
                //update
                movieDb.Id = updateMovieDto.Id;
                movieDb.Title = updateMovieDto.Title;
                movieDb.Description = updateMovieDto.Description;
                movieDb.Year = updateMovieDto.Year;
                movieDb.Genre = updateMovieDto.Genre;
                //return result
                return StatusCode(StatusCodes.Status204NoContent, "Movie updated");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        // Delete a movie
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                // validation
                if (id <= 0)
                {
                    return BadRequest("Id has invalid value");
                }
                // match the parameter id with the movie id
                Movie movieToDelete = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                // checking if an id is entered
                if (movieToDelete == null)
                {
                    return NotFound($"Movie with id {id} was not found!");
                }
                // remove
                StaticDb.Movies.Remove(movieToDelete);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }
    }
}
