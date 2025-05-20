using HW4ServerSide.DAL;
using HW4ServerSide.DTO;
using HW4ServerSide.Models;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW4ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentMovieController : ControllerBase
    {
        // GET: api/<RentMovieController>
        [HttpGet("{userId}")]
        public IEnumerable<RentedMoviesDTO> Get(int userId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetCurrentlyRentedMoviesByUser(userId);
        }

        [HttpGet("GetRentedMovies")]
        public IActionResult GetRentedMovies(int userId)
        {
            try
            {
                DBservices dbs = new DBservices();
                List<Movie> movies = dbs.GetMovieByUser(userId);
                if (movies == null || movies.Count == 0)
                    return NotFound("No currently rented movies found for this user.");
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST api/<RentMovieController>
        [HttpPost]
        public IActionResult Post([FromBody] RentedMoviesDTO movie)
        {
            DBservices dbs = new DBservices();
            try
            {
                dbs.InsertRentedMovie(movie);
                return Ok(new { success = true, message = "Rented movie added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

        }

        // PUT api/<RentMovieController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RentMovieController>/5
        [HttpDelete("{userId}/{movieId}")]
        public IActionResult Delete(int userId, int movieId)
        {
            DBservices dbs = new DBservices();
            try
            {
                int result = dbs.DeleteRentedMovies(userId, movieId);
                if (result > 0)
                {
                    return Ok(new { success = true, message = $"Rented movie with id {movieId}" +
                    $" with user id {userId} Have been deleted", movieIdReturn = movieId });
                }
                else
                {
                    return NotFound(new { success = false, message = $"No Rented movie with movie id {movieId}" +
                    $" and user id {movieId} have been found."});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
