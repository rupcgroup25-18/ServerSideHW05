using HW4ServerSide.Models;
using HW4ServerSide.DAL;
using Microsoft.AspNetCore.Mvc;
using HW4ServerSide.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW4ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        // GET: api/<MovieController>
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            DBservices dBservices = new DBservices();
            return dBservices.ReadMovies();
        }

        [HttpGet("GetByTitle")]
        public IEnumerable<Movie> GetByTitle(string title)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetMoviesByTitle(title);

        }

        [HttpGet("GetByReleaseDate/from/{startDate}/until/{endDate}")]
        public IEnumerable<Movie> GetByReleaseDate(DateTime startDate, DateTime endDate)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetMoviesByReleaseDate(startDate, endDate);
        }

        // POST api/<MovieController>
        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            DBservices dbs = new DBservices();

            try
            {
                dbs.InsertMovie(movie);
                return Ok(new { success = true, message = "Movie added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Failed to add movie: {ex.Message}" });
            }

        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            DBservices dbs = new DBservices();

            try
            {
                movie.Id = id;
                int result = dbs.UpdateMovie(movie);
                if (result > 0)
                {
                    Console.WriteLine("Movie updated successfully.");
                    return Ok("Movie updated successfully.");
                }
                else
                {
                    Console.WriteLine("No movie found with the given Id.");
                    return NotFound("No movie found with the given Id.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DBservices dbs = new DBservices();

            try
            {
                int result = dbs.DeleteMovie(id);
                if (result > 0)
                {
                    Console.WriteLine($"Movie with Id {id} deleted successfully.");
                    return Ok($"Movie with Id {id} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"No movie with Id {id} found.");
                    return NotFound($"No movie with Id {id} found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}