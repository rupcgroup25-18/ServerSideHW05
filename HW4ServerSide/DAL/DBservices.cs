using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using HW4ServerSide.Models;
using HW4ServerSide.Controllers;
using HW4ServerSide.DTO;

namespace HW4ServerSide.DAL
{
    public class DBservices
    {
        public DBservices()
        {

        }

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the appsettings.json 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(string conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        public int InsertMovie(Movie movie)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@URL", movie.Url);
            paramDic.Add("@description", movie.Description);
            paramDic.Add("@primaryTitle", movie.PrimaryTitle);
            paramDic.Add("@primaryImage", movie.PrimaryImage);
            paramDic.Add("@year", movie.Year);
            paramDic.Add("@releaseDate", movie.ReleaseDate);
            paramDic.Add("@language", movie.Language);
            paramDic.Add("@budget", movie.Budget);
            paramDic.Add("@isAdult", movie.IsAdult);
            paramDic.Add("@grossWorldwide", movie.GrossWorldwide);
            paramDic.Add("@genres", movie.Genres);
            paramDic.Add("@runtimeMinutes", movie.RuntimeMinutes);
            paramDic.Add("@averageRating", movie.AverageRating);
            paramDic.Add("@numVotes", movie.NumVotes);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertMovie", con, paramDic); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        public int UpdateMovie(Movie movie)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Id", movie.Id);
            paramDic.Add("@URL", movie.Url);
            paramDic.Add("@description", movie.Description);
            paramDic.Add("@primaryTitle", movie.PrimaryTitle);
            paramDic.Add("@primaryImage", movie.PrimaryImage);
            paramDic.Add("@year", movie.Year);
            paramDic.Add("@releaseDate", movie.ReleaseDate);
            paramDic.Add("@language", movie.Language);
            paramDic.Add("@budget", movie.Budget);
            paramDic.Add("@isAdult", movie.IsAdult);
            paramDic.Add("@grossWorldwide", movie.GrossWorldwide);
            paramDic.Add("@genres", movie.Genres);
            paramDic.Add("@runtimeMinutes", movie.RuntimeMinutes);
            paramDic.Add("@averageRating", movie.AverageRating);
            paramDic.Add("@numVotes", movie.NumVotes);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_UpdateMovie", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public int DeleteMovie(int id)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Id", id);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_DeleteMovie", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public int InsertUser(Users user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Name", user.Name);
            paramDic.Add("@Email", user.Email);
            paramDic.Add("@Password", Users.HashPassword(user.Password)); // Always hash the password
            paramDic.Add("@Active", user.Active ? 1 : 0);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertUserNew", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public int UpdateUser(Users user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Id", user.Id);
            paramDic.Add("@Name", user.Name);
            paramDic.Add("@Email", user.Email);
            paramDic.Add("@Password", Users.HashPassword(user.Password)); // Hash again if password is updated
            paramDic.Add("@Active", user.Active);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_UpdateUserNew", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public int DeleteUser(int id)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Id", id);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_DeleteUserNew", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public int InsertRentedMovie(RentedMoviesDTO movie)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@userId", movie.UserId);
            paramDic.Add("@movieId", movie.MovieId);
            paramDic.Add("@rentStart", movie.RentStart);
            paramDic.Add("@rentEnd", movie.RentEnd);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertRentedMovie", con, paramDic); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Primary key violation
                {
                    throw new Exception("This movie has already been rented by this user.");
                }
                throw new Exception("An error occurred while inserting the movie.", ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int DeleteRentedMovies(int userId, int movieId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserId", userId);
            paramDic.Add("@MovieId", movieId);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_DeleteRentedMovies", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<Users> ReadUsers()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureGeneral("SP_SelectUserNew", con, null);

            List<Users> users = new List<Users>();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {
                while (dr.Read())
                {
                    Users u = new Users();
                    u.Id = Convert.ToInt32(dr["Id"]);
                    u.Name = dr["Name"].ToString();
                    u.Email = dr["Email"].ToString();
                    u.Password = dr["Password"].ToString();
                    u.Active = Convert.ToBoolean(dr["Active"]);
                    users.Add(u);
                }
                return users;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public List<Movie> ReadMovies()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureGeneral("SP_SelectMovie", con, null);

            List<Movie> movies = new List<Movie>();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {
                while (dr.Read())
                {
                    Movie m = new Movie();
                    m.Id = Convert.ToInt32(dr["ID"]);
                    m.Url = dr["URL"].ToString();
                    m.Description = dr["Description"].ToString();
                    m.PrimaryTitle = dr["PrimaryTitle"].ToString();
                    m.PrimaryImage = dr["PrimaryImage"].ToString();
                    m.Year = Convert.ToInt32(dr["Year"]);
                    m.ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"]);
                    m.Language = dr["Language"].ToString();
                    m.Budget = Convert.ToDouble(dr["Budget"]);
                    m.IsAdult = Convert.ToBoolean(dr["IsAdult"]);
                    m.GrossWorldwide = Convert.ToDouble(dr["GrossWorldwide"]);
                    m.Genres = dr["Genres"].ToString();
                    m.RuntimeMinutes = Convert.ToInt32(dr["RuntimeMinutes"]);
                    m.AverageRating = (float)Convert.ToDouble(dr["AverageRating"]);
                    m.NumVotes = Convert.ToInt32(dr["NumVotes"]);
                    m.PriceToRent = Convert.ToInt32(dr["priceToRent"]);
                    movies.Add(m);
                }
                return movies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public List<Movie> GetMovieByUser(int userId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserId", userId);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GetMovieByUser", con, paramDic);

            List<Movie> movies = new List<Movie>();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {
                while (dr.Read())
                {
                    Movie m = new Movie();
                    m.Id = Convert.ToInt32(dr["ID"]);
                    m.Url = dr["URL"].ToString();
                    m.Description = dr["Description"].ToString();
                    m.PrimaryTitle = dr["PrimaryTitle"].ToString();
                    m.PrimaryImage = dr["PrimaryImage"].ToString();
                    m.Year = Convert.ToInt32(dr["Year"]);
                    m.ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"]);
                    m.Language = dr["Language"].ToString();
                    m.Budget = dr["Budget"] != DBNull.Value ? Convert.ToDouble(dr["Budget"]) : 0.0;
                    m.IsAdult = Convert.ToBoolean(dr["IsAdult"]);
                    m.GrossWorldwide = dr["GrossWorldwide"] != DBNull.Value ? Convert.ToDouble(dr["GrossWorldwide"]) : 0.0;
                    m.Genres = dr["Genres"].ToString();
                    m.RuntimeMinutes = Convert.ToInt32(dr["RuntimeMinutes"]);
                    m.AverageRating = (float)Convert.ToDouble(dr["AverageRating"]);
                    m.NumVotes = Convert.ToInt32(dr["NumVotes"]);
                    m.PriceToRent = Convert.ToInt32(dr["pricetoRent"]);
                    movies.Add(m);
                }

                return movies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<RentedMoviesDTO> GetCurrentlyRentedMoviesByUser(int userId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserId", userId);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GetCurrentlyRentedMoviesByUser", con, paramDic);

            List<RentedMoviesDTO> movies = new List<RentedMoviesDTO>();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {
                while (dr.Read())
                {
                    RentedMoviesDTO m = new RentedMoviesDTO();
                    m.UserId = Convert.ToInt32(dr["userId"]);
                    m.MovieId = Convert.ToInt32(dr["movieId"]);
                    m.RentStart = Convert.ToDateTime(dr["rentStart"]);
                    m.RentEnd = Convert.ToDateTime(dr["rentEnd"]);
                    movies.Add(m);
                }

                return movies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<Movie> GetMoviesByReleaseDate(DateTime startDate, DateTime endDate)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@StartDate", startDate);
            paramDic.Add("@EndDate", endDate);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GetMoviesByReleaseDate", con, paramDic);

            List<Movie> movies = new List<Movie>();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {
                while (dr.Read())
                {
                    Movie m = new Movie();
                    m.Id = Convert.ToInt32(dr["ID"]);
                    m.Url = dr["URL"].ToString();
                    m.Description = dr["Description"].ToString();
                    m.PrimaryTitle = dr["PrimaryTitle"].ToString();
                    m.PrimaryImage = dr["PrimaryImage"].ToString();
                    m.Year = Convert.ToInt32(dr["Year"]);
                    m.ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"]);
                    m.Language = dr["Language"].ToString();
                    m.Budget = dr["Budget"] != DBNull.Value ? Convert.ToDouble(dr["Budget"]) : 0.0;
                    m.IsAdult = Convert.ToBoolean(dr["IsAdult"]);
                    m.GrossWorldwide = dr["GrossWorldwide"] != DBNull.Value ? Convert.ToDouble(dr["GrossWorldwide"]) : 0.0;
                    m.Genres = dr["Genres"].ToString();
                    m.RuntimeMinutes = Convert.ToInt32(dr["RuntimeMinutes"]);
                    m.AverageRating = (float)Convert.ToDouble(dr["AverageRating"]);
                    m.NumVotes = Convert.ToInt32(dr["NumVotes"]);
                    m.PriceToRent = Convert.ToInt32(dr["pricetoRent"]);
                    movies.Add(m);
                }
                return movies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<Movie> GetMoviesByTitle(string Title)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Title", Title);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GetMovieByTitle", con, paramDic);

            List<Movie> movies = new List<Movie>();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {
                while (dr.Read())
                {
                    Movie m = new Movie();
                    m.Id = Convert.ToInt32(dr["ID"]);
                    m.Url = dr["URL"].ToString();
                    m.Description = dr["Description"].ToString();
                    m.PrimaryTitle = dr["PrimaryTitle"].ToString();
                    m.PrimaryImage = dr["PrimaryImage"].ToString();
                    m.Year = Convert.ToInt32(dr["Year"]);
                    m.ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"]);
                    m.Language = dr["Language"].ToString();
                    m.Budget = dr["Budget"] != DBNull.Value ? Convert.ToDouble(dr["Budget"]) : 0.0;
                    m.IsAdult = Convert.ToBoolean(dr["IsAdult"]);
                    m.GrossWorldwide = dr["GrossWorldwide"] != DBNull.Value ? Convert.ToDouble(dr["GrossWorldwide"]) : 0.0;
                    m.Genres = dr["Genres"].ToString();
                    m.RuntimeMinutes = Convert.ToInt32(dr["RuntimeMinutes"]);
                    m.AverageRating = (float)Convert.ToDouble(dr["AverageRating"]);
                    m.NumVotes = Convert.ToInt32(dr["NumVotes"]);
                    m.PriceToRent = Convert.ToInt32(dr["pricetoRent"]);
                    movies.Add(m);
                }
                return movies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommandWithStoredProcedureGeneral(string spName, SqlConnection con, Dictionary<string, object> paramDic)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con; // assign the connection to the command object

            cmd.CommandText = spName; // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 30; // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = CommandType.StoredProcedure; // the type of the command, can also be text

            if (paramDic != null)
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }

            return cmd;
        }
    }
}